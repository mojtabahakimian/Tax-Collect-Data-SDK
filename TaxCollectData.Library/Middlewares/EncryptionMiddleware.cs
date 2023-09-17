using TaxCollectData.Library.Abstraction.Cryptography;
using TaxCollectData.Library.Cryptography;

namespace TaxCollectData.Library.Middlewares;

public class EncryptionMiddleware : Middleware
{
    private readonly IEncryptor _encryptor;

    public EncryptionMiddleware(IEncryptor encryptor)
    {
        _encryptor = encryptor;
    }

    public override string Handle(string text)
    {
        return HandleNext(_encryptor.Encrypt(text));
    }
}