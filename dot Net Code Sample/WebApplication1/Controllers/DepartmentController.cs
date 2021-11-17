using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [CustomExceptionFilterAttribute]
    [Route("api/[controller]")]

    //[RoutePrefix("api/v1/Registration")]

    [ApiController]
    public class DepartmentController : Controller
    {
        public readonly WebApplication1Context web;

        public DepartmentController (WebApplication1Context context)
        {
            web = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> DepartmentIndex()
        {
            return await web.Department.ToListAsync();
        }

        //[HttpGet("GetDepartment/{id}")]
        //public async Task<ActionResult<Department>> GetDepartment(int id)
        //{
        //    var department = await web.Department.FindAsync(id);

        //    if (department == null)
        //    {
        //        return NotFound();
        //    }

        //    return department;
        //}

        [HttpGet("GetDepartment/{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {

            return web.Department.FromSqlRaw<Department>("departmentlist {0}", id).ToList().FirstOrDefault();
        }

        [HttpPost("DepartmentCreate")]
        public async Task <ReturnResponse> DepartmentCreate(Department department)
        {
            ReturnResponse response = new ReturnResponse();

            department.InsertedDate = DateTime.Now;
            
            web.Department.Add(department);
            await web.SaveChangesAsync();

            response.status = true;
            response.Message = "Date Insert Successfull";


            return response;
        }

        [HttpPost("DepartmentEdit/{id}")]
        public async Task <ReturnResponse> DepartmentEdit(int id, Department department)
        {
            ReturnResponse response = new ReturnResponse();

            

            var Data = await web.Department.FirstOrDefaultAsync(x => x.DepartmentID == id);
            if(Data != null)
            {
                Convert.ToInt32(string.Empty);
                Data.DepoartmentName = department.DepoartmentName;
                Data.PrimaryContactNumber = department.PrimaryContactNumber;
                Data.UpdatedDate = DateTime.Now;
                Data.IsActive = department.IsActive;

                await web.SaveChangesAsync();

                response.status = true;
                response.Message = "Date Updated SuccessFully";
            }
            return response;
        }

        [HttpPost("DepartmentDelete/{id}")]
        public async Task <ReturnResponse> DepartmentDelete(int id)
        {
            ReturnResponse response = new ReturnResponse();

            Convert.ToInt32(string.Empty);

            var list = await web.Department.FirstOrDefaultAsync(x => x.DepartmentID == id);

            web.Department.Remove(list);

            await web.SaveChangesAsync();

            response.status = true;
           response.Message = "Data Delete SuccessFull";

            return response;
        }
        
    }
}
