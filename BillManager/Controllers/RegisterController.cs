using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillManager.Models;
using BillManager.Models.ModelsDTO;
using BillManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BillManager.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUsersService _usersService;
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterController> logger,
            RoleManager<IdentityRole> roleManager,
            IUsersService usersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _usersService = usersService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ResponseAfterAutDTO> Register([FromBody] UserDTO user)
        {
            var newUser = new ApplicationUser()
            {
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                IsPaid = false,
                PhoneNumber = user.PhoneNumber.ToString()
            };

            var result = await _userManager.CreateAsync(newUser, user.PasswordHash);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    var role = new IdentityRole("Admin");
                    var res = await _roleManager.CreateAsync(role);

                    if (res.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, "Admin");
                    }
                }
                else
                {
                    if (!await _roleManager.RoleExistsAsync("User"))
                    {
                        var role = new IdentityRole("User");
                        await _roleManager.CreateAsync(role);
                    }
                    await _userManager.AddToRoleAsync(newUser, "User");
                }

                _logger.LogInformation("User created a new account with password.");
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                _logger.LogInformation("User is logged");

                return _usersService.GetIdAndRoleForUserById(user.Email);
            }

            _logger.LogInformation("User register failed.");

            return new ResponseAfterAutDTO() { Code = 400, Message = "Loggin failed", Status = "Failed" };
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseAfterAutDTO> Login([FromBody] UserDTO user)
        {
            var user1 = await _userManager.FindByNameAsync(user.UserName);
            
            if (user1 != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user1, user.PasswordHash, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return _usersService.GetIdAndRoleForUserById(user.Email);
                }
                else
                {
                    _logger.LogInformation("User logged failed.");
                    return new ResponseAfterAutDTO { Code = 400, Message = "Loggin failed", Status = "Failed" };
                }
            }
            else
            {
                _logger.LogInformation("User logged failed.");
                return new ResponseAfterAutDTO { Code = 400, Message = "Loggin failed", Status = "Failed" };
            }

        }




        //   [HttpGet]
        //[AllowAnonymous]
        //public async Task<ResponseAfterAutDTO> LogOut()
        //{
        //    await _signInManager.SignOutAsync();
        //    _logger.LogInformation("User logged out.");
        //    return new ResponseAfterAutDTO { Code = 200, Message = "LogOut", Status = "Success" };
        //}
    }
}

