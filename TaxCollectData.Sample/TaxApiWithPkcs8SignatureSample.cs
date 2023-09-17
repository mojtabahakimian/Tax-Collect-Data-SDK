using TaxCollectData.Library.Abstraction.Clients;
using TaxCollectData.Library.Factories;
using TaxCollectData.Library.Properties;

namespace TaxCollectData.Sample;

internal class TaxApiWithPkcs8SignatureSample
{
    private const string ClientId = "A1117Z";

    private const string BaseUrl = "http://master.nta.local/requestsmanager";
    // private const string BaseUrl = "http://localhost:8055/requestsmanager/api";
    private const string PrivateKeyPath = @"MohammadAminSalemi_expire.key";
    private const string CertificatePath = @"MohammadAminSalemi_expire.cer";

    private readonly SampleTest _sampleTest = new();

    public void Run()
    {
        var taxApi = GetTaxApi(out var publicApi);
        _sampleTest.Run(ClientId, publicApi, taxApi);
    }

    private ITaxApi GetTaxApi(out ITaxPublicApi publicApi)
    {
        var pkcs8SignatoryFactory = new Pkcs8SignatoryFactory();
        var encryptorFactory = new EncryptorFactory();
        var properties = new TaxProperties(ClientId);
        var taxApiFactory = new TaxApiFactory(BaseUrl, properties);
        var signatory = pkcs8SignatoryFactory.Create(PrivateKeyPath, CertificatePath);
        publicApi = taxApiFactory.CreatePublicApi(signatory);
        var encryptor = encryptorFactory.Create(publicApi);
        return taxApiFactory.CreateApi(signatory, encryptor);
    }
}