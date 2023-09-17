using TaxCollectData.Library.Abstraction.Clients;
using TaxCollectData.Library.Abstraction.Cryptography;
using TaxCollectData.Library.Cryptography;
using TaxCollectData.Library.Models;
using TaxCollectData.Library.Repositories;

namespace TaxCollectData.Library.Factories;

public class EncryptorFactory
{
    public IEncryptor Create(ITaxPublicApi publicApi)
    {
        return Create(() => publicApi.GetServerInformation().PublicKeys);
    }

    public IEncryptor Create(Func<List<KeyModel>> getPublicKeys)
    {
        var encryptionKeyRepository = new EncryptionKeyRepository(getPublicKeys);
        return new JweEncryptor(encryptionKeyRepository);
    }
}