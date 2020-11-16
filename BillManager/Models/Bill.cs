using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Models
{
    //public enum Months 
    //{ 
    //    January, February, March, April, May, June,
    //    July, August, September, October,November, December
    //}
    public class Bill
    {
        public int Id { get; set; }
        //public Months Months { get; set; }
        public string Year { get; set; }
        public string Name { get; set; }
        public string January { get; set; }
        public string February { get; set; }
        public string March { get; set; }
        public string April { get; set; }
        public string May { get; set; }
        public string June { get; set; }
        public string July { get; set; }
        public string August { get; set; }
        public string September { get; set; }
        public string October { get; set; }
        public string November { get; set; }
        public string December { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
