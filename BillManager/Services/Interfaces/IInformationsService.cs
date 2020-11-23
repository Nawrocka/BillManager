using BillManager.Models.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Services.Interfaces
{
    public interface IInformationsService
    {
        ResponseDTO AddInformation(InformationDTO informationDTO);
        ResponseDTO EditInformation(InformationDTO informationDTO);
        ResponseDTO DeleteInformation(string email);
        InformationsDTO GetAllInformationsByUser(string email);
    }
}
