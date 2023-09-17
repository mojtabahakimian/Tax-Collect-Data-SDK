using Microsoft.VisualStudio.Threading;
using TaxCollectData.Library.Abstraction.Clients;
using TaxCollectData.Library.Dto;
using TaxCollectData.Library.Models;
using TaxCollectData.Library.Abstraction.Providers;

namespace TaxCollectData.Library.Clients;

public class LowLevelTaxApi : ILowLevelTaxApi
{
    private readonly IClient _client;
    private readonly IRequestProvider _requestProvider;

    public LowLevelTaxApi(IClient client, IRequestProvider requestProvider)
    {
        _client = client;
        _requestProvider = requestProvider;
    }

    public ServerInformationModel GetServerInformation()
    {
        using var taskContext = new JoinableTaskContext();
        var taskFactory = new JoinableTaskFactory(taskContext);
        return taskFactory.Run(async () => await GetServerInformationAsync().ConfigureAwait(true));
    }

    public async Task<ServerInformationModel> GetServerInformationAsync()
    {
        var request = _requestProvider.GetServerInformation();
        return await SendRequestAsync<ServerInformationModel>(request).ConfigureAwait(false);
    }


    public FiscalFullInformationModel GetFiscalInformation(string memoryId)
    {
        using var taskContext = new JoinableTaskContext();
        var taskFactory = new JoinableTaskFactory(taskContext);
        return taskFactory.Run(async () => await GetFiscalInformationAsync(memoryId).ConfigureAwait(true));
    }

    public async Task<FiscalFullInformationModel> GetFiscalInformationAsync(string memoryId)
    {
        var request = _requestProvider.GetFiscalInformationRequest(memoryId);
        return await SendRequestAsync<FiscalFullInformationModel>(request).ConfigureAwait(false);
    }

    public TaxpayerModel GetTaxpayer(string economicCode)
    {
        using var taskContext = new JoinableTaskContext();
        var taskFactory = new JoinableTaskFactory(taskContext);
        return taskFactory.Run(async () => await GetTaxpayerAsync(economicCode).ConfigureAwait(true));
    }

    public async Task<TaxpayerModel> GetTaxpayerAsync(string economicCode)
    {
        var request = _requestProvider.GetTaxpayerRequest(economicCode);
        return await SendRequestAsync<TaxpayerModel>(request).ConfigureAwait(false);
    }


    public BatchResponseModel SendInvoices(List<PacketDto> packets)
    {
        using var taskContext = new JoinableTaskContext();
        var taskFactory = new JoinableTaskFactory(taskContext);
        return taskFactory.Run(async () => await SendInvoicesAsync(packets).ConfigureAwait(true));
    }

    public async Task<BatchResponseModel> SendInvoicesAsync(List<PacketDto> packets)
    {
        var request = _requestProvider.GetInvoicesRequest(packets);
        return await SendRequestAsync<BatchResponseModel>(request).ConfigureAwait(false);
    }


    public List<InquiryResultModel> InquiryByTime(InquiryByTimeRangeDto dto)
    {
        using var taskContext = new JoinableTaskContext();
        var taskFactory = new JoinableTaskFactory(taskContext);
        return taskFactory.Run(async () => await InquiryByTimeAsync(dto).ConfigureAwait(true));
    }

    public async Task<List<InquiryResultModel>> InquiryByTimeAsync(InquiryByTimeRangeDto dto)
    {
        var request = _requestProvider.GetInquiryByTimeRequest(dto);
        return await SendRequestAsync<List<InquiryResultModel>>(request).ConfigureAwait(false);
    }


    public List<InquiryResultModel> InquiryByUid(InquiryByUidDto dto)
    {
        using var taskContext = new JoinableTaskContext();
        var taskFactory = new JoinableTaskFactory(taskContext);
        return taskFactory.Run(async () => await InquiryByUidAsync(dto).ConfigureAwait(true));
    }

    public async Task<List<InquiryResultModel>> InquiryByUidAsync(InquiryByUidDto dto)
    {
        var request = _requestProvider.GetInquiryByUidRequest(dto);
        return await SendRequestAsync<List<InquiryResultModel>>(request).ConfigureAwait(false);
    }


    public List<InquiryResultModel> InquiryByReferenceId(InquiryByReferenceNumberDto dto)
    {
        using var taskContext = new JoinableTaskContext();
        var taskFactory = new JoinableTaskFactory(taskContext);
        return taskFactory.Run(async () => await InquiryByReferenceIdAsync(dto).ConfigureAwait(true));
    }

    public async Task<List<InquiryResultModel>> InquiryByReferenceIdAsync(InquiryByReferenceNumberDto dto)
    {
        var request = _requestProvider.GetInquiryByReferenceIdRequest(dto);
        return await SendRequestAsync<List<InquiryResultModel>>(request).ConfigureAwait(false);
    }

    private async Task<T> SendRequestAsync<T>(HttpRequestMessage request)
    {
        var nonceRequest = _requestProvider.GetNonceRequest();
        return await _client.SendRequestAsync<T>(request, nonceRequest).ConfigureAwait(false);
    }
}