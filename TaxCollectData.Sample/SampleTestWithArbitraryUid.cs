using TaxCollectData.Library.Abstraction.Cryptography;
using TaxCollectData.Library.Clients;
using TaxCollectData.Library.Configs;
using TaxCollectData.Library.Dto;
using TaxCollectData.Library.Factories;
using TaxCollectData.Library.Models;
using TaxCollectData.Library.Properties;
using System.Text;
using System.Text.Json;
using TaxCollectData.Library.Abstraction.Clients;

namespace TaxCollectData.Sample;

public class SampleTestWithArbitraryUid
{
    private const string ClientId = "A11226";
    private const string BaseUrl = "http://master.nta.local/requestsmanager";
    private const string PrivateKeyPath = @"privatekey1.pem";
    private const string CertificatePath = @"cert1.crt";
    private void TestSendWithUid(InvoiceDto invoice, string uid)
    {
        TaxApiFactory taxApiFactory = new TaxApiFactory(BaseUrl, new TaxProperties(ClientId));
        ISignatory signatory = new Pkcs8SignatoryFactory().Create(PrivateKeyPath, CertificatePath);
        ITaxPublicApi publicApi = taxApiFactory.CreatePublicApi(signatory);
        IEncryptor encryptor = new EncryptorFactory().Create(publicApi);
        LowLevelTaxApi lowLevelApi = taxApiFactory.CreateLowLevelApi(signatory);
        BatchResponseModel responseModel = lowLevelApi.SendInvoices(new List<PacketDto>
        {
            GetPacket(invoice, uid, signatory, encryptor)
        });
    }
    
    private PacketDto GetPacket(InvoiceDto invoiceDto, string uid, ISignatory signatory, IEncryptor encryptor)
    {
        string payload = encryptor.Encrypt(signatory.Sign(Serialize(invoiceDto)));
        PacketHeaderDto packetHeaderDto = new PacketHeaderDto
        {
            RequestTraceId = uid,
            FiscalId = ClientId
        };
        return new PacketDto
        {
            Header = packetHeaderDto,
            Payload = payload
        };
    }
    
    private string Serialize(object dto)
    {
        return Encoding.UTF8.GetString(JsonSerializer.SerializeToUtf8Bytes(dto, JsonSerializerConfig.JsonSerializerOptions));
    }
}