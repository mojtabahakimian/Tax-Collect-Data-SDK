namespace TaxCollectData.Library.Abstraction.Providers;

public interface ITaxIdProvider
{
    string GenerateTaxId(string memoryId, long serial, DateTime createDate);
}