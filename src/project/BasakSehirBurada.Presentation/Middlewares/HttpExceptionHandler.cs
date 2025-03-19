using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
namespace BasakSehirBurada.Presentation.Middlewares;

public class HttpExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";
       
        
        if (exception is NotFoundException notFound)
        {

         
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            NotFoundProblemDetails problemDetails = new NotFoundProblemDetails(notFound.Message);
            string json = JsonSerializer.Serialize(problemDetails);

            await httpContext.Response.WriteAsync(json);

            return false;
        }


        if (exception is BusinessException business)
        {
            
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            BusinessProblemDetails problemDetails = new BusinessProblemDetails(business.Message);
            string json = JsonSerializer.Serialize(problemDetails);

            await httpContext.Response.WriteAsync(json);

            return false;
        }

        if(exception is AuthorizationException authorization)
        {

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            AuthorizationProblemDetails problemDetails = new AuthorizationProblemDetails(authorization.Errors);
            string json = JsonSerializer.Serialize(problemDetails);

            await httpContext.Response.WriteAsync(json);

            return false;
        }


        return true;
    }
}
