namespace TaxCollectData.Library.Models;

public class InvoiceErrorModel
{
    public InvoiceErrorModel(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }
    public string Message { get; }
}