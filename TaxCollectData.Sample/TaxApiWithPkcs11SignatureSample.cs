using TaxCollectData.Library.Abstraction.Clients;
using TaxCollectData.Library.Factories;
using TaxCollectData.Library.Properties;

namespace TaxCollectData.Sample;

internal class TaxApiWithPkcs11SignatureSample
{
    private const string ClientId = "A11226";

    // private const string BaseUrl = "http://master.nta.local/requestsmanager/api";
    private const string BaseUrl = "http://localhost:8055/requestsmanager/api";
    private const string TokenSerialNumber = "2da4b5e60001000d7ca6";

    private readonly SampleTest _sampleTest = new();

    public void Run()
    {
        var taxApi = GetTaxApi(out var publicApi);
        _sampleTest.Run(ClientId, publicApi, taxApi);
    }

    private ITaxApi GetTaxApi(out ITaxPublicApi publicApi)
    {
        var pkcs11SignatoryFactory = new Pkcs11SignatoryFactory();
        var encryptorFactory = new EncryptorFactory();

        var properties = new TaxProperties(ClientId);
        var taxApiFactory = new TaxApiFactory(BaseUrl, properties);

        var pkcs11LibraryPath = @$"{AppContext.BaseDirectory}Cryptography\Pkcs11\ShuttleCsp11_3003.dll";
        var signatory = pkcs11SignatoryFactory.Create(TokenSerialNumber, pkcs11LibraryPath, "1234");
        publicApi = taxApiFactory.CreatePublicApi(signatory);
        var encryptor = encryptorFactory.Create(publicApi);

        return taxApiFactory.CreateApi(signatory, encryptor);
    }
}