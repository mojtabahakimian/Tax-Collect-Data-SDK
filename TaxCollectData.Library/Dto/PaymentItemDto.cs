
namespace TaxCollectData.Library.Dto;

public class PaymentItemDto {
    public string iinn { get; set; }
    public string acn { get; set; }
    public string trmn { get; set; }
    public string trn { get; set; }
    public string pcn { get; set; }
    public string pid { get; set; }
    public long? pdt { get; set; }
    public int? pmt { get; set; }
    public long? pv { get; set; }
}