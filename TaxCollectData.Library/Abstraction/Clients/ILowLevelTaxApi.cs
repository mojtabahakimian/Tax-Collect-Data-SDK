using TaxCollectData.Library.Dto;
using TaxCollectData.Library.Models;

namespace TaxCollectData.Library.Abstraction.Clients;

public interface ILowLevelTaxApi
{
    ServerInformationModel GetServerInformation();
    Task<ServerInformationModel> GetServerInformationAsync();
    BatchResponseModel SendInvoices(List<PacketDto> packets);
    Task<BatchResponseModel> SendInvoicesAsync(List<PacketDto> packets);
    FiscalFullInformationModel GetFiscalInformation(string memoryId);
    Task<FiscalFullInformationModel> GetFiscalInformationAsync(string memoryId);
    TaxpayerModel GetTaxpayer(string economicCode);
    Task<TaxpayerModel> GetTaxpayerAsync(string economicCode);
    List<InquiryResultModel> InquiryByTime(InquiryByTimeRangeDto dto);
    Task<List<InquiryResultModel>> InquiryByTimeAsync(InquiryByTimeRangeDto dto);
    List<InquiryResultModel> InquiryByUid(InquiryByUidDto dto);
    Task<List<InquiryResultModel>> InquiryByUidAsync(InquiryByUidDto dto);
    List<InquiryResultModel> InquiryByReferenceId(InquiryByReferenceNumberDto dto);
    Task<List<InquiryResultModel>> InquiryByReferenceIdAsync(InquiryByReferenceNumberDto dto);
}