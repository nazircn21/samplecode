using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ErrorLog
    {
        public int ID { get; set; }
        public string ErrorSource { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorTrace { get; set; }
        public DateTime? ErrorDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
