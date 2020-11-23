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

namespace BillManager.Services
{
    public class InformationsService : IInformationsService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public InformationsService(ApplicationDbContext context, ILogger<InformationsService> logger, IMapper mapper)
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
        }
        public ResponseDTO AddInformation(InformationDTO informationDTO)
        {
            _logger.LogInformation("Executing AddInformation method");

            try
            {
                _context.Information.Add(_mapper.Map<Information>(informationDTO));
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = e.Message,
                    Status = "Error during add information"
                };
            }

            return new ResponseDTO()
            {
                Code = 200,
                Message = "Added information to db",
                Status = "Success"
            };
        }

        public ResponseDTO DeleteInformation(string email)
        {
            _logger.LogInformation("Executing DeleteBill method");

            var informationToRemove = _context.Information.Where(i => i.User.Email == email).SingleOrDefault();

            if (informationToRemove == null)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = $"Information of the user's email { email } doesn't exist in db",
                    Status = "Error"
                };
            }

            try
            {
                _context.Information.Remove(informationToRemove);
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = e.Message,
                    Status = "Error during delete information"
                };
            }

            return new ResponseDTO()
            {
                Code = 200,
                Message = "Delete information from db",
                Status = "Success"
            };
        }

        public ResponseDTO EditInformation(InformationDTO informationDTO)
        {
            _logger.LogInformation("Executing EditBill method");

            if (_context.Information.Where(b => b.Id == informationDTO.Id).Count() == 0)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = $"Information of the user's id { informationDTO.Id} doesn't exist in db",
                    Status = "Error"
                };
            }
            try
            {
                _context.Information.Update(_mapper.Map<Information>(informationDTO));
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = e.Message,
                    Status = "Error"
                };
            }

            return new ResponseDTO()
            {
                Code = 200,
                Message = "Edit information in db",
                Status = "Success"
            };
        }

        public InformationsDTO GetInformationsByUser(string email)
        {
            _logger.LogInformation("Executing GetInformationsByUser method");

            List<Information> informations = _context.Information.Where(i => i.User.Email == email).ToList();

            InformationsDTO informationDTO = new InformationsDTO() { };
            informationDTO.InformationsList = new List<InformationDTO>();

            foreach (Information information in informations)
            {
                informationDTO.InformationsList.Add(_mapper.Map<InformationDTO>(information));
            }

            return informationDTO;
        }
    }
}
