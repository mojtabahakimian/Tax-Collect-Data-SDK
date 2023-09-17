namespace TaxCollectData.Library.Exceptions;

public class NotInitializedException : Exception
{
    public NotInitializedException()
    {
    }

    public NotInitializedException(Exception cause) : base(cause.Message, cause)
    {
    }

    public NotInitializedException(string paramName) : base(GetMessage(paramName))
    {
    }

    public NotInitializedException(string message, Exception cause) : base(message, cause)
    {
    }

    private static string GetMessage(string paramName)
    {
        return $"Parameter {paramName} is not initialized.";
    }
}