using TaxCollectData.Library.Dto;

namespace TaxCollectData.Library.Abstraction.Providers;

public interface IPacketProvider
{
    PacketDto CreateInvoicePacket(InvoiceDto invoice);
}