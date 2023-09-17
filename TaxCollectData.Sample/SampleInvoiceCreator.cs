using TaxCollectData.Library.Abstraction.Providers;
using TaxCollectData.Library.Dto;
using TaxCollectData.Library.Factories;

namespace TaxCollectData.Sample;

internal class SampleInvoiceCreator
{
    private readonly ITaxIdProvider _taxIdProvider;

    public SampleInvoiceCreator()
    {
        _taxIdProvider = new TaxIdProviderFactory().Create();
    }

    public List<InvoiceDto> Create(string clientId, int count)
    {
        //Generate Random Serial number
        var random = new Random();
        var randomSerialDecimal = random.NextInt64(1000000000, 9999999999);
        var now = new DateTimeOffset(DateTime.Now);
        var taxId = _taxIdProvider.GenerateTaxId(clientId, randomSerialDecimal, DateTime.Now);

        var invoiceDto = new InvoiceDto
        {
            Body = new() { GetInvoiceBodyDto() },
            Header = GetInvoiceHeaderDto(now.ToUnixTimeMilliseconds(), taxId, randomSerialDecimal),
            Payments = new()
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
            sstid = "2710000138624",
            sstt = "سرسیلندر قطعات صنعت فولاد سازی",
            mu = "164",
            am = 2,
            fee = 10000,
            prdis = 20000,
            dis = 500,
            adis = 19500,
            vra = 9,
            vam = 1755,
            tsstam = 21255
        };
        return body;
    }

    private static HeaderDto GetInvoiceHeaderDto(long now, string taxId, long randomSerialDecimal)
    {
        var header = new HeaderDto
        {
            inty = 1,
            inp = 1,
            inno = randomSerialDecimal.ToString("X").PadLeft(10, '0'),
            ins = 1,
            tob = 2,
            bid = "10100302746",
            tins = "14003778990",
            tinb = "10100302746",
            tprdis = 20000,
            tadis = 19500,
            tdis = 500,
            tvam = 1755,
            todam = 0,
            tbill = 21255,
            setm = 2,
            indatim = now,
            taxid = taxId
        };
        return header;
    }
}