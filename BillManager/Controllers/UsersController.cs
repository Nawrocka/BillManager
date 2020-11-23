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
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {        
        private readonly IUsersService _usersService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUsersService usersService, ILogger<UsersController> logger)
        {
            this._usersService = usersService;
            this._logger = logger;
        }

        [HttpGet]
        [Route("/api/users/getAll")]
        public IActionResult GetAllUsers()
        {
            _logger.LogInformation("Executing GetAllUsers controller");
            return Ok(_usersService.GetAllUsers());
        }

        [HttpPut]
        [Route("/api/users/edit")]
        public ActionResult EditUser([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation("Executing EditUser controller");
            return Ok(_usersService.EditUser(userDTO));
        }
    }
}
