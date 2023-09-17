using TaxCollectData.Library.Abstraction.Providers;
using TaxCollectData.Library.Algorithms;
using TaxCollectData.Library.Providers;

namespace TaxCollectData.Library.Factories;

public class TaxIdProviderFactory
{
    public ITaxIdProvider Create()
    {
        return new TaxIdProvider(GetErrorDetectionAlgorithm());
    }

    private IErrorDetectionAlgorithm GetErrorDetectionAlgorithm()
    {
        return new VerhoeffAlgorithm();
    }
}