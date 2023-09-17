namespace TaxCollectData.Library.Dto;

public class InvoiceDto
{
    public HeaderDto Header { get; set; }
    public List<BodyItemDto> Body { get; set; }
    public List<PaymentItemDto> Payments { get; set; }
    public List<ExtensionItemDto> Extension { get; set; }
}