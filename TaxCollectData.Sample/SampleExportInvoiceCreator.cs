using TaxCollectData.Library.Abstraction.Providers;
using TaxCollectData.Library.Dto;
using TaxCollectData.Library.Factories;

namespace TaxCollectData.Sample;

internal class SampleExportInvoiceCreator
{
    private readonly ITaxIdProvider _taxIdProvider;

    public SampleExportInvoiceCreator()
    {
        _taxIdProvider = new TaxIdProviderFactory().Create();
    }

    public List<InvoiceDto> Create(string clientId, int count)
    {
        //Generate Random Serial number
        var random = new Random();
        long randomSerialDecimal = random.Next(999999999);
        var now = new DateTimeOffset(DateTime.Now);
        var taxId = _taxIdProvider.GenerateTaxId(clientId, randomSerialDecimal, DateTime.Now);

        var invoiceDto = new InvoiceDto
        {
            Body = new() { GetInvoiceBodyDto() },
            Header = GetInvoiceHeaderDto(now.ToUnixTimeMilliseconds(), taxId, randomSerialDecimal),
            Payments = new() { GetPaymentDto() }
        };
        return Enumerable.Range(0, count).Select(_ => invoiceDto).ToList();
    }

    private static PaymentItemDto GetPaymentDto()
    {
        var payment = new PaymentItemDto
        {
            iinn = "1131244211",
            acn = "2131244212",
            trmn = "3131244213",
            trn = "4131244214"
        };
        return payment;
    }

    private static BodyItemDto GetInvoiceBodyDto()
    {
        var body = new BodyItemDto
        {
            sstid = "1111111111",
            sstt = "شیر کم چرب پاستوریزه",
            mu = "006584",
            am = 2,
            fee = 500_000,
            prdis = 500_000,
            dis = 0,
            adis = 500_000,
            vra = 0,
            vam = 0,
            tsstam = 1000_000,
            nw = 1000_000,
            ssrv = 1000_000,
            sscv = 1000_000
        };
        return body;
    }

    private static HeaderDto GetInvoiceHeaderDto(long now, string taxId, long randomSerialDecimal)
    {
        var header = new HeaderDto
        {
            inty = 1,
            inp = 7,
            inno = randomSerialDecimal.ToString(),
            ins = 1,
            tins = "14003778990",
            tprdis = 1000_000,
            tadis = 1000,
            tdis = 0,
            tvam = 0,
            todam = 0,
            tbill = 1000_000,
            setm = 1,
            cap = 1000_000,
            insp = 1000_000,
            tvop = 0,
            tax17 = 0,
            indatim = now,
            indati2m = now,
            taxid = taxId,
            cdcd = 12345,
            cdcn = "12345",
            tonw = 1000_000,
            torv = 1000_000,
            tocv = 1000_000
        };
        return header;
    }
}