using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillManager.Models.ModelsDTO;
using BillManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BillManager.Controllers
{
    public class InformationController : Controller
    {
        private readonly IInformationsService _informationsService;
        private readonly ILogger<InformationController> _logger;
        public InformationController(IInformationsService informationsService, ILogger<InformationController> logger)
        {
            this._informationsService = informationsService;
            this._logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/api/information/add")]
        public IActionResult AddInformation([FromBody] InformationDTO informationDTO)
        {
            _logger.LogInformation("Executing AddInformation controller");
            return Ok(_informationsService.AddInformation(informationDTO));
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("/api/information/edit")]
        public ActionResult EditInformation([FromBody] InformationDTO informationDTO)
        {
            _logger.LogInformation("Executing EditInformation controller");
            return Ok(_informationsService.EditInformation(informationDTO));
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("/api/information/delete/{email}")]
        public IActionResult DeleteInformation(string email)
        {
            _logger.LogInformation("Executing DeleteInformation controller");
            return Ok(_informationsService.DeleteInformation(email));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/api/information/getAll/{email}")]
        public IActionResult GetAllByUser(string email)
        {
            _logger.LogInformation("Executing GetAllByUser controller");
            return Ok(_informationsService.GetAllInformationsByUser(email));
        }
    }
}
