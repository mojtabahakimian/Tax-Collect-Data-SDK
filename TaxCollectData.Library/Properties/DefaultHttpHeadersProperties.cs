using TaxCollectData.Library.Abstraction.Properties;

namespace TaxCollectData.Library.Properties;

public class DefaultHttpHeadersProperties : IHttpHeadersProperties
{
    private const string Authorization = "Authorization";

    public string AuthorizationHeaderName => Authorization;

    public IDictionary<string, string> CustomHeaders => new Dictionary<string, string>();
}