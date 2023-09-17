namespace TaxCollectData.Library.Exceptions;

public class PacketTypeNotFoundException : Exception
{
    public PacketTypeNotFoundException()
    {
    }

    public PacketTypeNotFoundException(Exception cause) : base(cause.Message, cause)
    {
    }

    public PacketTypeNotFoundException(string paramName) : base(paramName)
    {
    }

    public PacketTypeNotFoundException(string message, Exception cause) : base(message, cause)
    {
    }
}