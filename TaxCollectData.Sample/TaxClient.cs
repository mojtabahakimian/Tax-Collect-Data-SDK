using TaxCollectData.Library.Abstraction.Clients;
using TaxCollectData.Library.Algorithms;
using TaxCollectData.Library.Dto;
using TaxCollectData.Library.Enums;
using TaxCollectData.Library.Factories;
using TaxCollectData.Library.Properties;
using TaxCollectData.Library.Providers;

namespace TaxCollectData.Sample;

public static class TaxClient
{
    private const string MemoryId = "A111YO";
    private const string ApiUrl = "http://master.nta.local/requestsmanager";
    private const string PrivateKeyPath = @"privatekey1.pem";
    private const string CertificatePath = @"cert1.crt";

    public static void Run()
    {
        var taxApi = CreateTaxApi();

        var validInvoice = CreateValidInvoice();
        var invalidInvoice = CreateInvalidInvoice();

        var invoiceList = new List<InvoiceDto>()
        {
            validInvoice,
            invalidInvoice
        };

        var responseModels = taxApi.SendInvoices(invoiceList);

        Thread.Sleep(10_000);
        
        DateTime startDate = DateTime.Now.AddDays(-1).ToLocalTime();
        DateTime endDate = DateTime.Now.ToLocalTime();

        var inquiryResultModels = taxApi.InquiryByTime(new(startDate, endDate, null, RequestStatus.PENDING));
        var inquiryResultModels2 = taxApi.InquiryByTime(new(startDate, endDate, null, RequestStatus.SUCCESS));

        var referenceNumbers = responseModels.Select(r => r.ReferenceNumber).ToList();
        var inquiryDto = new InquiryByReferenceNumberDto(referenceNumbers, startDate, endDate);
        var inquiryResults = taxApi.InquiryByReferenceId(inquiryDto);
    }

    private static ITaxApi CreateTaxApi()
    {
        var pkcs8SignatoryFactory = new Pkcs8SignatoryFactory();
        var encryptorFactory = new EncryptorFactory();
        var properties = new TaxProperties(MemoryId);
        var taxApiFactory = new TaxApiFactory(ApiUrl, properties);
        var signatory = pkcs8SignatoryFactory.Create(PrivateKeyPath, CertificatePath);
        var publicApi = taxApiFactory.CreatePublicApi(signatory);
        var encryptor = encryptorFactory.Create(publicApi);
        return taxApiFactory.CreateApi(signatory, encryptor);
    }

    private static InvoiceDto CreateValidInvoice()
    {
        var random = new Random();
        var serial = random.NextInt64(1_000_000_000);
        var now = DateTime.Now;
        var taxId = GenerateTaxId(serial, now);
        var inno = serial.ToString("X").PadLeft(10, '0');
        var indatim = new DateTimeOffset(now).ToUnixTimeMilliseconds();

        var invoice = new InvoiceDto()
        {
            Header = new HeaderDto()
            {
                taxid = taxId,
                inno = inno,
                indatim = indatim,
                inty = 1,
                inp = 1,
                ins = 1,
                tins = "14003778990",
                tinb = "10100302746",
                tprdis = 20_000,
                tdis = 500,
                tadis = 19_500,
                tvam = 1_755,
                todam = 0, 
                tbill = 21_255,
                setm = 2
            },
            Body = new List<BodyItemDto>()
            {
                new()
                {
                    sstid = "2710000138624",
                    sstt = "سرسیلندر قطعات صنعت فولاد سازی",
                    mu = "164",
                    am = 2,
                    fee = 10_000,
                    prdis = 20_000,
                    dis = 500,
                    adis = 19_500,
                    vra = 9,
                    vam = 1_755,
                    tsstam = 21255
                }
            }
        };
        return invoice;
    }

    private static InvoiceDto CreateInvalidInvoice()
    {
        var random = new Random();
        var serial = random.NextInt64(1_000_000_000);
        var now = DateTime.Now;
        var taxId = GenerateTaxId(serial, now);
        var inno = serial.ToString("X").PadLeft(10, '0');
        var indatim = new DateTimeOffset(now).ToUnixTimeMilliseconds();

        var invoice = new InvoiceDto()
        {
            Header = new HeaderDto()
            {
                taxid = taxId,
                inno = inno,
                indatim = indatim,
                inty = 1,
                inp = 7,
                ins = 1,
                tins = "14003778990",
                tinb = "10100302746",
                tprdis = 30_000,
                tdis = 500,
                tadis = 19_500,
                tvam = 1_765,
                todam = 1_000,
                tbill = 21_255,
                setm = 3
            },
            Body = new List<BodyItemDto>()
            {
                new()
                {
                    sstid = "1710000138624",
                    sstt = "کالای اشتباه",
                    mu = "164",
                    am = 2,
                    fee = 10_000,
                    prdis = 20_000,
                    dis = 0,
                    adis = 19_500,
                    vra = 10,
                    vam = 0,
                    tsstam = 20_000
                }
            }
        };
        return invoice;
    }

    private static string GenerateTaxId(long serial, DateTime now)
    {
        var taxIdProvider = new TaxIdProvider(new VerhoeffAlgorithm());
        return taxIdProvider.GenerateTaxId(MemoryId, serial, now);
    }
}