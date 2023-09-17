namespace TaxCollectData.Library.Dto;

public class InquiryByReferenceNumberDto
{
    public InquiryByReferenceNumberDto(List<string> referenceNumbers, DateTime? start = null, DateTime? end = null)
    {
        ReferenceNumbers = referenceNumbers;
        Start = start;
        End = end;
    }

    public List<string> ReferenceNumbers { get; }
    public DateTime? Start { get; }
    public DateTime? End { get; }
}