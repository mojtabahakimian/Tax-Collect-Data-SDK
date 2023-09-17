namespace TaxCollectData.Library.Abstraction.Properties;

public interface IHttpHeadersProperties
{
    string AuthorizationHeaderName { get; }
    IDictionary<string, string> CustomHeaders { get; }
}