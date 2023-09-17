using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;
using TaxCollectData.Library.Abstraction.Cryptography;
using Microsoft.Extensions.Logging;
using TaxCollectData.Library.Providers;
using JWT.Builder;
using JWT.Algorithms;
using Net.Pkcs11Interop.X509Store;

namespace TaxCollectData.Library.Cryptography;

public class JwsHardwareTokenSignatory : ISignatory
{
    private readonly X509Certificate2 _parsedCertificate;
    private readonly RS512Algorithm _algorithm;
    private readonly ILogger? _logger;
    private const string Jose = "jose";
    private const string SigT = "sigT";
    private const string Typ = "typ";
    private const string Crit = "crit";
    private const string Cty = "cty";
    private const string ContentTypeHeaderValue = "text/plain";


    public JwsHardwareTokenSignatory(string certificateAlias,
        string pkcs11LibraryPath,
        string pin,
        ILogger? logger = null)
    {
        var pkcs11X509Certificate = GetCertificateFromStore(certificateAlias, pkcs11LibraryPath, pin);
        _parsedCertificate = pkcs11X509Certificate.Info.ParsedCertificate;
        _algorithm = new RS512Algorithm(pkcs11X509Certificate.GetRSAPublicKey(), pkcs11X509Certificate.GetRSAPrivateKey());
        _logger = logger;
    }
    
    public JwsHardwareTokenSignatory(RS512Algorithm algorithm,
        X509Certificate2 certificate,
        ILogger? logger = null)
    {
        _parsedCertificate = certificate;
        _algorithm = algorithm;
        _logger = logger;
    }

    private Pkcs11X509Certificate GetCertificateFromStore(string certificateAlias, string pkcs11LibraryPath, string pin)
    {
        var store = new Pkcs11X509Store(pkcs11LibraryPath, new ETokenPinProvider(pin));
        return store.Slots[0]
            .Token
            .Certificates
            .First(c => c.Info.Label.Equals(certificateAlias, StringComparison.InvariantCultureIgnoreCase));
    }

    public string Sign(string text)
    {
        var stopwatch = Stopwatch.StartNew();

        var token = JwtBuilder.Create()
            .DoNotVerifySignature()
            .WithAlgorithm(_algorithm)
            .AddHeader(HeaderName.X5c, new[] {Convert.ToBase64String(_parsedCertificate.GetRawCertData())})
            .AddHeader(SigT, new CurrentDateProvider().ToDateFormat())
            .AddHeader(Typ, Jose)
            .AddHeader(Crit, new[] {SigT})
            .AddHeader(Cty, ContentTypeHeaderValue)
            .Encode(JsonSerializer.Deserialize<JsonNode>(text));
        
        _logger?.LogDebug("sign in {} ms", stopwatch.ElapsedMilliseconds);

        return token;
    }

    public string Sign(object data)
    {
        var stopwatch = Stopwatch.StartNew();

        var token = JwtBuilder.Create()
            .DoNotVerifySignature()
            .WithAlgorithm(_algorithm)
            .AddHeader(HeaderName.X5c, new[] {Convert.ToBase64String(_parsedCertificate.GetRawCertData())})
            .AddHeader(SigT, new CurrentDateProvider().ToDateFormat())
            .AddHeader(Typ, Jose)
            .AddHeader(Crit, new[] {SigT})
            .AddHeader(Cty, ContentTypeHeaderValue)
            .Encode(data);
        
        _logger?.LogDebug("sign in {} ms", stopwatch.ElapsedMilliseconds);

        return token;
    }


    private class ETokenPinProvider : IPinProvider
    {
        private readonly string _pin;

        public ETokenPinProvider(string pin)
        {
            _pin = pin;
        }

        private GetPinResult GetPin()
        {
            return new GetPinResult(false, _pin.Select((ch) => (byte) ch).ToArray()); //Convert to bytes
        }

        public GetPinResult GetKeyPin(
            Pkcs11X509StoreInfo storeInfo,
            Pkcs11SlotInfo slotInfo,
            Pkcs11TokenInfo tokenInfo,
            Pkcs11X509CertificateInfo certificateInfo
        )
        {
            return GetPin();
        }

        public GetPinResult GetTokenPin(
            Pkcs11X509StoreInfo storeInfo,
            Pkcs11SlotInfo slotInfo,
            Pkcs11TokenInfo tokenInfo
        )
        {
            return GetPin();
        }
    }
}