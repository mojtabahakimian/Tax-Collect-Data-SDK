

using TaxCollectData.Library.Abstraction.Properties;

namespace TaxCollectData.Library.Properties;

public class GsbUrlProperties : IUrlProperties {

    private const string Version = "v2";
    private const string Api = "api";


    private readonly string _baseUrl;

    public GsbUrlProperties(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    public string GetUrl(string request)
    {
        return Path.Combine(_baseUrl, Api, Version, request);
    }
}