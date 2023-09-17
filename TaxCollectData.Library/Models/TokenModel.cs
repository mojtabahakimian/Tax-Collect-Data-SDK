namespace TaxCollectData.Library.Models;

public class TokenModel
{
    public TokenModel(string nonce, string clientId)
    {
        Nonce = nonce;
        ClientId = clientId;
    }

    public string Nonce { get; }
    public string ClientId { get; }
}