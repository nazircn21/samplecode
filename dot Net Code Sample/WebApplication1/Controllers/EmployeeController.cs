using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data;
using WebApplication1.Filters;
using WebApplication1.Models;
//using WebApplication1.Filters;

namespace WebApplication1.Controllers
{

    [Route("api/[controller]")]

    [ApiController]
    [CustomExceptionFilterAttribute]

    public class EmployeeController : ControllerBase
    {
        private readonly WebApplication1Context _context;

        public EmployeeController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> Employee()
        {
            //Convert.ToInt32(string.Empty);
            return await _context.EmployeeModel.ToListAsync();
        }

        //[HttpGet("GetEmployee/{id}")]
        //public async Task<ActionResult<EmployeeModel>> GetEmployee(int id)
        //{
        //    var employee = await _context.EmployeeModel.FindAsync(id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return employee;
        //}

        [HttpGet("GetEmployee/{id}")]
        public async Task <ActionResult<EmployeeModel>> GetEmployee(int id)
        {
            return _context.EmployeeModel.FromSqlRaw<EmployeeModel>("Employeelist {0}", id).ToList().FirstOrDefault();
        }

        [HttpPost("Employee")]
        public async Task <ReturnResponse> Employee(EmployeeModel employee)
        {
            ReturnResponse response = new ReturnResponse();

           
                Convert.ToInt32(string.Empty);
                employee.InsertedDate = DateTime.Now;

                _context.EmployeeModel.Add(employee);
                await _context.SaveChangesAsync();

                response.status = true;
                response.Message = "Registration Successfull";

                
            
            
            return response;

        }

        [HttpPost("Edit/{id}")]
        public async Task<ReturnResponse>  Edit(int id, EmployeeModel model)
        {
            ReturnResponse response = new ReturnResponse();
            try
            {
                var data = _context.EmployeeModel.Where(x => x.EmployeeID == id).FirstOrDefault();
                if (data != null)
                {
                    //Convert.ToInt32(string.Empty);
                    data.EmployeeName = model.EmployeeName;
                    data.EmployeeSalary = model.EmployeeSalary;
                    data.Address = model.Address;
                    data.City = model.City;
                    data.UpdatedDate = DateTime.Now;

                    await _context.SaveChangesAsync();

                    response.Message = "Employee Updated SuccessFully";
                    response.status = true;
                }
            }
            catch(Exception ex)
            {
                var controllerName = this.ControllerContext.RouteData.Values != null ? (string)this.ControllerContext.RouteData.Values["controller"] : string.Empty;
                var actionName = this.ControllerContext.RouteData.Values != null ? (string)this.ControllerContext.RouteData.Values["action"] : string.Empty;

                var errorlog = new ErrorLog();
               
                errorlog.ErrorSource = controllerName + "/" + actionName;
                //errorlog.ErrorMessage = ex.InnerException != null && ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : string.Empty;
                errorlog.ErrorMessage = ex.Message;
                errorlog.ErrorTrace = ex.StackTrace;
                errorlog.ErrorDate = DateTime.Now;
                errorlog.CreatedBy = 0;
                errorlog.CreatedDate = DateTime.Now;

                _context.ErrorLogs.Add(errorlog);

                await _context.SaveChangesAsync();

                response.status = false;
                response.Message = ex.Message;
            }
            return response;
        }


        [HttpPost("Delete/{id}")]
        //[Route("Delete")]
        public async Task <ReturnResponse> Delete(int id)
        {
            ReturnResponse response = new ReturnResponse();
            try
            {
                //Convert.ToInt32(string.Empty);
                var data = _context.EmployeeModel.Where(x => x.EmployeeID == id).FirstOrDefault();

                _context.EmployeeModel.Remove(data);
                await _context.SaveChangesAsync();

                response.Message = "Delete SuccessFully";
                response.status = true;
            }
            catch(Exception ex)
            {
                var controllerName = this.ControllerContext.RouteData.Values != null ? (string)this.ControllerContext.RouteData.Values["controller"] : string.Empty;
                var actionName = this.ControllerContext.RouteData.Values != null ? (string)this.ControllerContext.RouteData.Values["action"] : string.Empty;

                var errorlog = new ErrorLog();

                errorlog.ErrorSource = controllerName + "/" + actionName;
                //errorlog.ErrorMessage = ex.InnerException != null && ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : string.Empty;
                errorlog.ErrorMessage = ex.Message;
                errorlog.ErrorTrace = ex.StackTrace;
                errorlog.ErrorDate = DateTime.Now;
                errorlog.CreatedBy = 0;
                errorlog.CreatedDate = DateTime.Now;

                _context.ErrorLogs.Add(errorlog);

                await _context.SaveChangesAsync();

                response.status = false;
                response.Message = ex.Message;
            }

            return response;
        }

    }
}
