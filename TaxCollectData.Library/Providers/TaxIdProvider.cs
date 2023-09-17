using System.Text;
using TaxCollectData.Library.Abstraction.Providers;
using TaxCollectData.Library.Algorithms;

namespace TaxCollectData.Library.Providers;

public class TaxIdProvider : ITaxIdProvider
{
    private readonly IErrorDetectionAlgorithm _errorDetectionAlgorithm;

    public TaxIdProvider(IErrorDetectionAlgorithm errorDetectionAlgorithm)
    {
        _errorDetectionAlgorithm = errorDetectionAlgorithm;
    }

    public string GenerateTaxId(string memoryId, long serial, DateTime createDate)
    {
        var timeDayRange = (int)(new DateTimeOffset(createDate).ToUnixTimeSeconds() / (3600 * 24));
        var hexTime = Convert.ToString(timeDayRange, 16);
        var hexSerial = Convert.ToString(serial, 16);
        var initial = $"{memoryId}{hexTime.PadLeft(5, '0')}{hexSerial.PadLeft(10, '0')}";
        var controlText = $"{ToDecimal(memoryId)}{timeDayRange.ToString().PadLeft(6, '0')}{serial.ToString().PadLeft(12, '0')}";
        var result = $"{initial}{_errorDetectionAlgorithm.GenerateCheckDigit(controlText)}";
        return result.ToUpperInvariant();
    }

    private static string ToDecimal(string memoryId)
    {
        var decimalFormat = new StringBuilder();
        foreach (var ch in memoryId)
        {
            if (char.IsDigit(ch))
            {
                decimalFormat.Append(ch);
            }
            else
            {
                decimalFormat.Append((int)ch);
            }
        }

        return decimalFormat.ToString();
    }
}