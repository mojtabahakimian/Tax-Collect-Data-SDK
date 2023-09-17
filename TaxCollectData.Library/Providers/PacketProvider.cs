using TaxCollectData.Library.Abstraction.Cryptography;
using TaxCollectData.Library.Abstraction.Providers;
using TaxCollectData.Library.Dto;
using TaxCollectData.Library.Middlewares;
using TaxCollectData.Library.Properties;

namespace TaxCollectData.Library.Providers;

public class PacketProvider : AbstractPacketProvider, IPacketProvider
{
    private readonly EncryptionMiddleware _encryptionMiddleware;
    private readonly SignatoryMiddleware _signatoryMiddleware;
    private readonly EmptyMiddleware _emptyMiddleware;
    private readonly ISerializer _serializer;

    public PacketProvider(TaxProperties packetProperties,
        ISignatory signatory,
        IEncryptor encryptor,
        ISerializer serializer) : base(packetProperties)
    {
        _serializer = serializer;
        _encryptionMiddleware = new EncryptionMiddleware(encryptor);
        _signatoryMiddleware = new SignatoryMiddleware(signatory);
        _emptyMiddleware = new EmptyMiddleware();
    }


    public PacketDto CreateInvoicePacket(InvoiceDto invoice)
    {
        return CreatePrivatePacket(invoice);
    }

    private PacketDto CreatePrivatePacket(object dto)
    {
        return GetPacketDto(dto, GetCryptographyMiddlewares());
    }

    private PacketDto CreateInquiryPacket(object dto)
    {
        return GetPacketDto(dto, GetEmptyMiddleware());
    }

    private Middleware GetEmptyMiddleware()
    {
        return Middleware.Link(_emptyMiddleware);
    }

    private Middleware GetCryptographyMiddlewares()
    {
        return Middleware.Link(_signatoryMiddleware, _encryptionMiddleware);
    }

    private PacketDto GetPacketDto(object data, Middleware middleware)
    {
        return new PacketDto
        {
            Payload = middleware.Handle(_serializer.Serialize(data)),
            Header = GetHeader()
        };
    }
}