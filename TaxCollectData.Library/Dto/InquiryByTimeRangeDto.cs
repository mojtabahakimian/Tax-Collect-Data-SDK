using TaxCollectData.Library.Enums;

namespace TaxCollectData.Library.Dto;

public class InquiryByTimeRangeDto
{
    public InquiryByTimeRangeDto(DateTime start, DateTime? end = null, Pageable? pageable = null, RequestStatus? status = null)
    {
        Start = start;
        End = end;
        Pageable = pageable;
        Status = status;
    }

    public DateTime Start { get; }
    public DateTime? End { get; }
    public Pageable? Pageable { get; }
    public RequestStatus? Status { get; }
}