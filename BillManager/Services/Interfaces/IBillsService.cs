using BillManager.Models.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Services.Interfaces
{
    public interface IBillsService
    {
        ResponseDTO AddBill(BillDTO billDTO);
        ResponseDTO EditBill(BillDTO billDTO);
        ResponseDTO DeleteBill(string email);
        BillsDTO GetAllBillsByUser(string email);        
    }
}
