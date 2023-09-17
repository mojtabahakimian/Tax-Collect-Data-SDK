namespace TaxCollectData.Library.Abstraction.Clients;

public interface IClient
{
    Task<T> SendRequestAsync<T>(HttpRequestMessage request, HttpRequestMessage nonceRequest);
}