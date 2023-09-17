using System.Text;
using System.Text.Json;
using TaxCollectData.Library.Abstraction.Providers;
using TaxCollectData.Library.Configs;

namespace TaxCollectData.Library.Providers;

public class Serializer : ISerializer
{
    public string Serialize(object dto)
    {
        return Encoding.UTF8.GetString(JsonSerializer.SerializeToUtf8Bytes(dto, JsonSerializerConfig.JsonSerializerOptions));
    }
}