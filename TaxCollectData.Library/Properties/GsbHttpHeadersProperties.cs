using TaxCollectData.Library.Abstraction.Properties;

namespace TaxCollectData.Library.Properties;

public class GsbHttpHeadersProperties : IHttpHeadersProperties
{
    private const string Token = "Token";

    public GsbHttpHeadersProperties(IDictionary<string, string> customHeaders)
    {
        CustomHeaders = customHeaders;
    }

    public string AuthorizationHeaderName => Token;

    public IDictionary<string, string> CustomHeaders { get; }
}