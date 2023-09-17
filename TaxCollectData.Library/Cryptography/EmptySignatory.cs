using TaxCollectData.Library.Abstraction.Cryptography;
using TaxCollectData.Library.Abstraction.Providers;

namespace TaxCollectData.Library.Cryptography;

public class EmptySignatory : ISignatory
{
    private readonly ISerializer _serializer;

    public EmptySignatory(ISerializer serializer)
    {
        _serializer = serializer;
    }

    public string Sign(string text)
    {
        return text;
    }

    public string Sign(object data)
    {
        return _serializer.Serialize(data);
    }
}