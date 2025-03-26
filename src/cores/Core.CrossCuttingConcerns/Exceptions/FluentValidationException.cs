namespace Core.CrossCuttingConcerns.Exceptions;

public class FluentValidationException : Exception
{


    public IEnumerable<ValidationExceptionModel> Errors { get; set; }
    public FluentValidationException(string message) : base(message)
    {
        
    }


    public FluentValidationException(IEnumerable<ValidationExceptionModel> errors) : base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }


    private static string BuildErrorMessage(IEnumerable<ValidationExceptionModel> errors)
    {

        var arr = errors.Select(x=> $"{x.Property} : {string.Join(" ", x.Errors)}");

        return string.Join("\n", arr);

    }
}
