
using System.Globalization;
using TaxCollectData.Library.Abstraction.Providers;

namespace TaxCollectData.Library.Providers;

public class CurrentDateProvider : ICurrentDateProvider {

    private const string Format = "yyyy-MM-dd'T'HH:mm:ss'Z'";

    public long ToEpochMilli() {
        return new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
    }

    public string ToDateFormat()
    {
        return DateTime.UtcNow.ToString(Format, CultureInfo.InvariantCulture);
    }
}