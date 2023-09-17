using TaxCollectData.Library.Models;

namespace TaxCollectData.Library.Abstraction.Clients;

public interface ITaxPublicApi
{
    ServerInformationModel GetServerInformation();
    Task<ServerInformationModel> GetServerInformationAsync();
}