using TaxCollectData.Library.Dto;

namespace TaxCollectData.Library.Abstraction.Providers;

public interface IRequestProvider
{
    HttpRequestMessage GetNonceRequest();

    HttpRequestMessage GetServerInformation();

    HttpRequestMessage GetInquiryByTimeRequest(InquiryByTimeRangeDto dto);

    HttpRequestMessage GetInquiryByUidRequest(InquiryByUidDto dto);

    HttpRequestMessage GetInquiryByReferenceIdRequest(InquiryByReferenceNumberDto dto);

    HttpRequestMessage GetInvoicesRequest(List<PacketDto> data);

    HttpRequestMessage GetTaxpayerRequest(string economicCode);

    HttpRequestMessage GetFiscalInformationRequest(string memoryId);
}