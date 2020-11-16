using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsPaid { get; set; }
        public IEnumerable<Bill> Bills { get; set; }
        public IEnumerable<Information> Informations { get; set; }
    }
}
