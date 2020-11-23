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
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public UsersService(ApplicationDbContext context, ILogger<UsersService> logger, IMapper mapper)
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
        }
        public ResponseDTO EditUser(UserDTO userDTO)
        {
            _logger.LogInformation("Executing EditUserByMail method");

            var user = _context.ApplicationUser.Where(b => b.Id == userDTO.Id).SingleOrDefault();

            if (user == null)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = $"ApplicationUser of the user's id { userDTO.Id } doesn't exist in db",
                    Status = "Error"
                };
            }

            user.IsPaid = userDTO.IsPaid;
            user.Email = userDTO.Email;
            user.UserName = userDTO.UserName;
            user.PasswordHash = userDTO.PasswordHash;
            user.PhoneNumber = userDTO.PhoneNumber;

            try
            {
                _context.ApplicationUser.Update(user);
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
                Message = "Edit applicationUser in db",
                Status = "Success"
            };
        }

        public UsersDTO GetAllUsers()
        {
            _logger.LogInformation("Executing GetAllUsers method");

            var users = _context.ApplicationUser.ToList();

            UsersDTO userDTO = new UsersDTO() { };
            userDTO.UsersList = new List<UserDTO>();

            foreach (ApplicationUser user in users)
            {
                userDTO.UsersList.Add(_mapper.Map<UserDTO>(user));
            }

            return userDTO;
        }

        public ResponseAfterAutDTO GetIdAndRoleForUserById(string email)
        {
            _logger.LogInformation("Executing GetIdAndRoleForUserById method");

            var user = _context.ApplicationUser.Where(u => u.Email == email).SingleOrDefault();
            var roleId = _context.UserRoles.Where(u => u.UserId == user.Id).FirstOrDefault().RoleId;
            var roleName = _context.Roles.Where(r => r.Id == roleId).SingleOrDefault().Name;
            var isAdmin = (roleName == "Admin") ? true : false;

            //Nie powinnam jakoś zwalidować ewentualne errory powyżej? Bo co jeśli nie ma usera o takim emailu?!
            return new ResponseAfterAutDTO()
            {
                Code = 200,
                Message = "User logged",
                Status = "Success",
                UserId = user.Id,
                Email = email,
                IsAdmin = isAdmin
            };
        }
    }
}
