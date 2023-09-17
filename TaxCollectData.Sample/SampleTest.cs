using TaxCollectData.Library.Abstraction.Clients;

namespace TaxCollectData.Sample;

public class SampleTest
{
    private readonly SampleInvoiceCreator _sampleInvoiceCreator = new();

    public void Run(string clientId, ITaxPublicApi publicApi, ITaxApi taxApi)
    {
        // var startDate = new DateTime(2023, 5, 1, 3, 4, 5, DateTimeKind.Local);
        // var endDate = new DateTime(2023, 5, 17, 3, 4, 5, DateTimeKind.Local);
        var serverInformation = publicApi.GetServerInformation();
        // var inquiryResultModels = taxApi.InquiryByTime(new(startDate)); 
        // var inquiryRangeResultModels = taxApi.InquiryByTime(new(startDate, endDate));
        var invoices = _sampleInvoiceCreator.Create(clientId, 1);
        var responsePacketModels = taxApi.SendInvoices(invoices);
        var referenceNumbers = responsePacketModels.Select(r => r.ReferenceNumber).ToList();
        var uidList = responsePacketModels.Select(r => r.Uid).ToList();
        Thread.Sleep(20000);
        var inquiryUidResultModels = taxApi.InquiryByUid(new(uidList, clientId));
        var inquiryByReferenceIdModels = taxApi.InquiryByReferenceId(new(referenceNumbers));
        var taxpayer = taxApi.GetTaxpayer("14003778990");
        var fiscalFullInformationModel = taxApi.GetFiscalInformation(clientId);
    }
}