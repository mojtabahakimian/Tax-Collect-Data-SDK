namespace TaxCollectData.Library.Models;

public class InquiryResultModel
{
    public string ReferenceNumber { get; set; }
    public string Uid { get; set; }
    public string Status { get; set; }
    public InvoiceValidationResponseModel Data { get; set; }
    public string PacketType { get; set; }
    public string FiscalId { get; set; }
}