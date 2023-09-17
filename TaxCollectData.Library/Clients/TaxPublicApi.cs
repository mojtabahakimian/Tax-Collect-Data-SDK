using TaxCollectData.Library.Abstraction.Clients;
using TaxCollectData.Library.Models;


namespace TaxCollectData.Library.Clients;

public class TaxPublicApi : ITaxPublicApi
{
    private readonly ILowLevelTaxApi _sender;

    public TaxPublicApi(ILowLevelTaxApi sender)
    {
        _sender = sender;
    }

    public ServerInformationModel GetServerInformation()
    {
        return _sender.GetServerInformation();
    }

    public async Task<ServerInformationModel> GetServerInformationAsync()
    {
        return await _sender.GetServerInformationAsync().ConfigureAwait(false);
    }
}