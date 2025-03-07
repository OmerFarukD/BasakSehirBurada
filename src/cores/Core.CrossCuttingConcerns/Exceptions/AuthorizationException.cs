namespace Core.CrossCuttingConcerns.Exceptions;

public class AuthorizationException : Exception
{

    public List<string> Errors { get; set; } = new List<string>();

    public AuthorizationException(string message) : base(message)
    {
        Errors.Add(message);
    }




    public AuthorizationException(List<string> errors) : base(BuildErrorMessgae(errors))
    {
        Errors = errors;
    }



    private static string BuildErrorMessgae(List<string> errors)
    {


        return string.Join("\n",errors);

    }

}
