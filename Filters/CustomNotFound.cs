using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

public class CustomNotFound : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is NotFoundException)
        {
            context.Result = new ObjectResult(
                new
                {
                    Message = context.Exception.Message,
                    StatusCode = (int)HttpStatusCode.NotFound,
                }
            )
            {
                StatusCode = (int)HttpStatusCode.NotFound,
            };

            context.ExceptionHandled = true;
        }
    }
}
