using TaxCollectData.Library.Abstraction.Clients;
using TaxCollectData.Library.Abstraction.Providers;
using TaxCollectData.Library.Dto;
using TaxCollectData.Library.Models;

namespace TaxCollectData.Library.Clients;

public class TaxApi : ITaxApi
{
    private readonly ILowLevelTaxApi _sender;
    private readonly IPacketProvider _packetFactory;

    public TaxApi(ILowLevelTaxApi sender, IPacketProvider packetFactory)
    {
        _sender = sender;
        _packetFactory = packetFactory;
    }

    public List<InquiryResultModel> InquiryByTime(InquiryByTimeRangeDto dto)
    {
        return _sender.InquiryByTime(dto);
    }

    public async Task<List<InquiryResultModel>> InquiryByTimeAsync(InquiryByTimeRangeDto dto)
    {
        return await _sender.InquiryByTimeAsync(dto).ConfigureAwait(false);
    }

    public List<InquiryResultModel> InquiryByUid(InquiryByUidDto dto)
    {
        return _sender.InquiryByUid(dto);
    }

    public async Task<List<InquiryResultModel>> InquiryByUidAsync(InquiryByUidDto dto)
    {
        return await _sender.InquiryByUidAsync(dto).ConfigureAwait(false);
    }


    public List<InquiryResultModel> InquiryByReferenceId(InquiryByReferenceNumberDto dto)
    {
        return _sender.InquiryByReferenceId(dto);
    }

    public async Task<List<InquiryResultModel>> InquiryByReferenceIdAsync(InquiryByReferenceNumberDto dto)
    {
        return await _sender.InquiryByReferenceIdAsync(dto).ConfigureAwait(false);
    }


    public FiscalFullInformationModel GetFiscalInformation(string memoryId)
    {
        return _sender.GetFiscalInformation(memoryId);
    }

    public async Task<FiscalFullInformationModel> GetFiscalInformationAsync(string memoryId)
    {
        return await _sender.GetFiscalInformationAsync(memoryId).ConfigureAwait(false);
    }

    public TaxpayerModel GetTaxpayer(string economicCode)
    {
        return _sender.GetTaxpayer(economicCode);
    }

    public async Task<TaxpayerModel> GetTaxpayerAsync(string economicCode)
    {
        return await _sender.GetTaxpayerAsync(economicCode).ConfigureAwait(false);
    }

    public List<InvoiceResponseModel> SendInvoices(List<InvoiceDto> invoices)
    {
        var invoicePackets = invoices.Select(GetInvoicePacket).ToList();
        var packets = invoicePackets.Select(i => i.PacketDto).ToList();
        var response = _sender.SendInvoices(packets).Result;
        var map = invoicePackets.ToDictionary(i => i.PacketDto.Header.RequestTraceId,
            i => i.TaxId);
        return response.Select(r => GetInvoiceResponseModel(map, r)).ToList();
    }

    public async Task<List<InvoiceResponseModel>> SendInvoicesAsync(List<InvoiceDto> invoices)
    {
        var invoicePackets = invoices.Select(GetInvoicePacket).ToList();
        var packets = invoicePackets.Select(i => i.PacketDto).ToList();
        var response = await _sender.SendInvoicesAsync(packets).ConfigureAwait(false);
        var map = invoicePackets.ToDictionary(i => i.PacketDto.Header.RequestTraceId,
            i => i.TaxId);
        return response.Result.Select(r => GetInvoiceResponseModel(map, r)).ToList();
    }

    private static InvoiceResponseModel GetInvoiceResponseModel(Dictionary<string, string> map, ResponsePacketModel x)
    {
        return new InvoiceResponseModel(x.Data, x.Uid, x.ReferenceNumber, map[x.Uid]);
    }

    private InvoicePacket GetInvoicePacket(InvoiceDto x)
    {
        return new InvoicePacket(x.Header.taxid, _packetFactory.CreateInvoicePacket(x));
    }
}