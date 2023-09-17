using TaxCollectData.Library.Abstraction.Clients;
using TaxCollectData.Library.Abstraction.Cryptography;
using TaxCollectData.Library.Abstraction.Properties;
using TaxCollectData.Library.Abstraction.Providers;
using TaxCollectData.Library.Clients;
using TaxCollectData.Library.Properties;
using TaxCollectData.Library.Providers;

namespace TaxCollectData.Library.Factories;

public class TaxApiFactory
{
    private readonly PacketProviderFactory _packetProviderFactory;
    private readonly IRequestProvider _requestProvider;
    private readonly TaxProperties _taxProperties;
    private readonly IHttpHeadersProperties _httpHeadersProperties;

    public TaxApiFactory(TaxProperties taxProperties,
        IRequestProvider requestProvider,
        IHttpHeadersProperties httpHeadersProperties)
    {
        _taxProperties = taxProperties;
        _requestProvider = requestProvider;
        _httpHeadersProperties = httpHeadersProperties;
        _packetProviderFactory = new PacketProviderFactory(taxProperties);
    }

    public TaxApiFactory(TaxProperties taxProperties, IUrlProperties urlProperties, IHttpHeadersProperties httpHeadersProperties)
        : this(taxProperties, new RequestProvider(urlProperties, new Serializer()), httpHeadersProperties)
    {
    }

    public TaxApiFactory(string baseUrl, TaxProperties taxProperties)
        : this(taxProperties, new RequestProvider(new DefaultUrlProperties(baseUrl), new Serializer()), new DefaultHttpHeadersProperties())
    {
    }

    public ITaxPublicApi CreatePublicApi(ISignatory signatory)
    {
        var lowLevelTaxApi = CreateLowLevelApi(signatory);
        return new TaxPublicApi(lowLevelTaxApi);
    }

    public ITaxApi CreateApi(ISignatory signatory, IEncryptor encryptor)
    {
        var lowLevelTaxApi = CreateLowLevelApi(signatory);
        var packetProvider = _packetProviderFactory.CreatePacketProvider(signatory, encryptor);
        return new TaxApi(lowLevelTaxApi, packetProvider);
    }

    public LowLevelTaxApi CreateLowLevelApi(ISignatory signatory)
    {
        return new LowLevelTaxApi(CreateRestHttpClient(signatory), _requestProvider);
    }

    private IClient CreateRestHttpClient(ISignatory signatory)
    {
        return new RestSharpHttpClient(GetHttpClient(), _taxProperties, signatory, _httpHeadersProperties, new Serializer());
    }
    
    private HttpClient GetHttpClient()
    {
        return new HttpClient
        {
            Timeout = TimeSpan.FromMinutes(10)
        };
    }
}