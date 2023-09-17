using TaxCollectData.Library.Dto;
using TaxCollectData.Library.Properties;

namespace TaxCollectData.Library.Providers;

public abstract class AbstractPacketProvider
{
    protected readonly TaxProperties PacketProperties;

    protected AbstractPacketProvider(TaxProperties packetProperties)
    {
        PacketProperties = packetProperties;
    }

    protected PacketHeaderDto GetHeader()
    {
        var requestTraceId = Guid.NewGuid().ToString();
        var fiscalId = PacketProperties.MemoryId;
        return new PacketHeaderDto
        {
            RequestTraceId = requestTraceId,
            FiscalId = fiscalId
        };
    }
}