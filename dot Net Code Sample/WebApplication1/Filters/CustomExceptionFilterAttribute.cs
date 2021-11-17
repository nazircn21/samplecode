using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        
       

        public override void OnException(ExceptionContext context)
        {
           

            var exception = context.Exception;
            context.Result = new ContentResult
            {
                Content = $"Error: {exception.Message}",
                ContentType = "text/plain",
                // change to whatever status code you want to send out
                StatusCode = (int?)HttpStatusCode.BadRequest



            };
            
            //var controllerName = context.RouteData.Values != null ? (string)context.RouteData.Values["controller"] : string.Empty;
            //var actionName = context.RouteData.Values != null ? (string)context.RouteData.Values["action"] : string.Empty;
           
            //    var errorLog = new ErrorLog();
            //    errorLog.ErrorSource = controllerName + "/" + actionName;
            //    errorLog.ErrorMessage = context.Exception.Message;
            //    errorLog.ErrorTrace = context.Exception.StackTrace;
            //    errorLog.ErrorDate = DateTime.Now;
            //    errorLog.CreatedBy = 0;
            //    errorLog.CreatedDate = DateTime.Now;
            //_context.ErrorLogs.Add(errorLog);
            //_context.SaveChanges();


        }
    
    }
}
    
    

