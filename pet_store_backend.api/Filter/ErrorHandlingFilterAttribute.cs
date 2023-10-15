using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace pet_store_backend.api.Filter
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var problemDetail = new ProblemDetails
            {
                Type ="https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "An error occurred while proccesing your request.",
                Status = (int)HttpStatusCode.InternalServerError, 
            };

            context.Result = new ObjectResult(problemDetail);

            context.ExceptionHandled = true;
        }
    }
}
