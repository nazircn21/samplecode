using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepoartmentName { get; set; }
        public string PrimaryContactNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
