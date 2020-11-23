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
    public class BillController : Controller
    {
        private readonly IBillsService _billService;
        private readonly ILogger<BillController> _logger;

        public BillController(IBillsService billsService, ILogger<BillController> logger)
        {
            this._billService = billsService;
            this._logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/api/bill/getAll/{email}")]
        public IActionResult GetAllBillsByUser(string email)
        {
            _logger.LogInformation("Executing GetAllBillsByUser controller");
            return Ok(_billService.GetAllBillsByUser(email));

        }
                
        [HttpPost]
        [Route("/api/bill/add")]
        public IActionResult AddBill([FromBody]BillDTO billDTO)
        {
            _logger.LogInformation("Executing AddBill controller");
            return Ok(_billService.AddBill(billDTO));
        }

        [HttpPut]
        [Route("/api/bill/edit")]
        public ActionResult EditBill([FromBody] BillDTO billDTO)
        {
            _logger.LogInformation("Executing EditBill controller");
            return Ok(_billService.EditBill(billDTO));
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("/api/bill/delete/{email}")]
        public IActionResult DeleteBill(string email)
        {
            _logger.LogInformation("Executing DeleteBill controller");
            return Ok(_billService.DeleteBill(email));
        }
    }
}
