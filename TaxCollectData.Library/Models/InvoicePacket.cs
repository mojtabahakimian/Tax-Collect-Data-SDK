using TaxCollectData.Library.Dto;

namespace TaxCollectData.Library.Models;

public class InvoicePacket
{
    public InvoicePacket(string taxId, PacketDto packetDto)
    {
        TaxId = taxId;
        PacketDto = packetDto;
    }

    public string TaxId { get; }
    public PacketDto PacketDto { get; }
}