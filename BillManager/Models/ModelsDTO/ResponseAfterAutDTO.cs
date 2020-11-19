using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Models.ModelsDTO
{
    public class ResponseAfterAutDTO :ResponseDTO
    {
        public bool IsAdmin { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
