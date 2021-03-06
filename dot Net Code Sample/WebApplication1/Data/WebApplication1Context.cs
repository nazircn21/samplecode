using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {
       

        public WebApplication1Context (DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }

        public DbSet<WebApplication1.Models.EmployeeModel> EmployeeModel { get; set; }
        public DbSet<WebApplication1.Models.ErrorLog> ErrorLogs { get; set; }
        public DbSet<WebApplication1.Models.Department> Department { get; set; }
    }
}
