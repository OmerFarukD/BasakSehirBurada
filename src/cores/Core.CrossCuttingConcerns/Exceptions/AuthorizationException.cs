namespace Core.CrossCuttingConcerns.Exceptions;

public class AuthorizationException : Exception
{

    public List<string> Errors { get; set; } = new List<string>();

    public AuthorizationException(string message) : base(message)
    {
        Errors.Add(message);
    }


    public AuthorizationException(List<string> errors)
    {
        Errors = errors;
    }

}
