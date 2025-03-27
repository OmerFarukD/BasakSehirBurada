using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class ValidationProblemDetails : ProblemDetails
{

    public IEnumerable<ValidationExceptionModel> Errors { get; set; }


    public ValidationProblemDetails(IEnumerable<ValidationExceptionModel> errors)
    {
        Errors = errors;
        Title = "Validataion Failure";
        Status = StatusCodes.Status400BadRequest;
    }
}
