using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HrApi;

public class CancellationTokenExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    // This will run AFTER the controller returns a response.
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is TaskCanceledException)
        {
            Console.WriteLine("Got that cancellation");
            context.Result = new ObjectResult(context.Exception.Message)
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }

    // This will run BEFORE your controller action is called.
    // "Fixing to execute your action"
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Nothing to see here. Carry on. 
    }
}
