using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using TaxCollectData.Library.Abstraction.Cryptography;
using TaxCollectData.Library.Abstraction.Providers;
using TaxCollectData.Library.Configs;

namespace TaxCollectData.Library.Cryptography;

public class JwsSignatory : ISignatory
{
    private const string Jose = "jose";
    private const string SigT = "sigT";
    private const string Typ = "typ";
    private const string Crit = "crit";
    private const string Cty = "cty";
    private const string ContentTypeHeaderValue = "text/plain";

    private readonly X509Certificate _certificate;
    private readonly RSA _privateKey;
    private readonly ICurrentDateProvider _currentDateProvider;
    private readonly ILogger? _logger;

    public JwsSignatory(X509Certificate certificate,
        RSA privateKey,
        ICurrentDateProvider currentDateProvider,
        ILogger? logger = null)
    {
        _certificate = certificate;
        _privateKey = privateKey;
        _currentDateProvider = currentDateProvider;
        _logger = logger;
    }

    public string Sign(string text)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            var publicKey = DotNetUtilities.ToRSA((RsaKeyParameters) DotNetUtilities.FromX509Certificate(_certificate).GetPublicKey());
            return JwtBuilder.Create()
                .WithJsonSerializer(new CustomSerializer())
                .WithAlgorithm(new RS256Algorithm(publicKey, _privateKey))
                .AddHeader(HeaderName.X5c, new[] {Convert.ToBase64String(_certificate.GetRawCertData())})
                .AddHeader(SigT, _currentDateProvider.ToDateFormat())
                .AddHeader(Typ, Jose)
                .AddHeader(Crit, new[] {SigT})
                .AddHeader(Cty, ContentTypeHeaderValue)
                .Encode(JsonSerializer.Deserialize<JsonNode>(text));
        }
        finally
        {
            _logger?.LogDebug("sign in {} ms", stopwatch.ElapsedMilliseconds);
        }
    }
    
    public string Sign(object data)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            var publicKey = DotNetUtilities.ToRSA((RsaKeyParameters) DotNetUtilities.FromX509Certificate(_certificate).GetPublicKey());
            return JwtBuilder.Create()
                .WithJsonSerializer(new CustomSerializer())
                .WithAlgorithm(new RS256Algorithm(publicKey, _privateKey))
                .AddHeader(HeaderName.X5c, new[] {Convert.ToBase64String(_certificate.GetRawCertData())})
                .AddHeader(SigT, _currentDateProvider.ToDateFormat())
                .AddHeader(Typ, Jose)
                .AddHeader(Crit, new[] {SigT})
                .AddHeader(Cty, ContentTypeHeaderValue)
                .Encode(data);
        }
        finally
        {
            _logger?.LogDebug("sign in {} ms", stopwatch.ElapsedMilliseconds);
        }
    }
    
    private class CustomSerializer : IJsonSerializer
    {
        public string Serialize(object obj)
        {
            return Encoding.UTF8.GetString(JsonSerializer.SerializeToUtf8Bytes(obj, JsonSerializerConfig.JsonSerializerOptions));
        }

        public object? Deserialize(Type type, string json)
        {
            return JsonSerializer.Deserialize(json, type);
        }
    }
}