using System.Security.Cryptography;

namespace TaxCollectData.Library.Abstraction.Repositories;

public interface IEncryptionKeyRepository
{
    RSA GetKey();
    string GetKeyId();
}