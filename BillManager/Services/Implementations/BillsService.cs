using AutoMapper;
using BillManager.Data;
using BillManager.Models;
using BillManager.Models.ModelsDTO;
using BillManager.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Services.Implementations
{
    public class BillsService : IBillsService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public BillsService(ApplicationDbContext context, ILogger<BillsService> logger, IMapper mapper)
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
        }
        public ResponseDTO AddBill(BillDTO billDTO)
        {
            _logger.LogInformation("Executing AddBill method");

            try
            {
                var mappedFrombillDTO = _mapper.Map<Bill>(billDTO);
                _context.Bill.Add(mappedFrombillDTO);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO() 
                {
                    Code = 400, 
                    Message = e.Message,
                    Status = "Error during add bill" 
                };         
            }
            return new ResponseDTO()
            {
                Code = 200,
                Message = "Added bill to db",
                Status = "Success"
            };
        }

        public ResponseDTO DeleteBill(string email)
        {
            _logger.LogInformation("Executing DeleteBill method");

            Bill billToDelete = _context.Bill.Where(b => b.User.Email == email).SingleOrDefault();

            if(billToDelete == null)
            {
                return new ResponseDTO() { Code = 400, Message = $"Bill of the user's email {email} doesn't exist in db", Status = "Error" };
            }

            try
            {
                _context.Bill.Remove(billToDelete);

            }
            catch (Exception e)
            {
                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Error during deleting bill" };
            }

            return new ResponseDTO() { Code = 200, Message = "Delete bill from db", Status = "Success" };
        }

        public ResponseDTO EditBill(BillDTO billDTO)
        {
            _logger.LogInformation("Executing EditBill method");
            
            if (_context.Bill.Where(bill => bill.Id == billDTO.Id).Count() == 0)
            {
                return new ResponseDTO() { Code = 400, Message = $"Bill of the users's id", Status = "Error" };
            }

            try
            {
                Bill mappedFrombillDTO = _mapper.Map<Bill>(billDTO);
                _context.Bill.Update(mappedFrombillDTO);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Error during updating bill" };
            }

            return new ResponseDTO() { Code = 200, Message = "Edite bill in db", Status = "Success" };
        }

        public BillsDTO GetAllBillsByUser(string email)
        {
            _logger.LogInformation("Executing GetAllBillsByUser method");

            List<Bill> bills = _context.Bill.Where(b => b.User.Email == email).ToList();
            BillsDTO billsDTO = new BillsDTO() { };
            billsDTO.BillsList = new List<BillDTO>();

            foreach(var bill in bills)
            {
                billsDTO.BillsList.Add(_mapper.Map<BillDTO>(bill));
            }

            billsDTO.BillsList.OrderBy(b => b.Year).Reverse().ToList();

            return billsDTO;
        }
    }
}
