using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;

namespace KeepNote.Aspect
{
    /*Override the methods of ActionFilterAttribute to log the information into file
     * at given file path.
    */
    public class LoggingAspect : ActionFilterAttribute
    {
        private string logFilePath;
        private string controllerName = string.Empty;
        private string actionName = string.Empty;
        private DateTime dateTime;
        private Stopwatch intertime = Stopwatch.StartNew();
        public LoggingAspect(IHostingEnvironment environment)
        {
            logFilePath = environment.ContentRootPath + @"/LogFile.txt";
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            intertime.Restart();
            dateTime = DateTime.Now;
        }

        public async override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            controllerName = filterContext.RouteData.Values["controller"].ToString();
            actionName = filterContext.RouteData.Values["action"].ToString();
            var message = string.Format("Controller Name: {0} Action Name: {1}, Request time (ms): {2}, duration to procress the request: {3}",controllerName, actionName, dateTime, intertime.ElapsedMilliseconds);
            await File.WriteAllTextAsync(logFilePath, message);
        }

    }
}
