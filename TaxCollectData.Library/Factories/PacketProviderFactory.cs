using TaxCollectData.Library.Abstraction.Cryptography;
using TaxCollectData.Library.Abstraction.Providers;
using TaxCollectData.Library.Properties;
using TaxCollectData.Library.Providers;

namespace TaxCollectData.Library.Factories;

public class PacketProviderFactory
{
    private readonly TaxProperties _packetProperties;

    public PacketProviderFactory(TaxProperties packetProperties)
    {
        _packetProperties = packetProperties;
    }

    public IPacketProvider CreatePacketProvider(ISignatory signatory, IEncryptor encryptor)
    {
        return new PacketProvider(_packetProperties, signatory, encryptor, new Serializer());
    }
}