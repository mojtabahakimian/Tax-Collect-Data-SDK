namespace TaxCollectData.Library.Models;

public class ServerInformationModel
{
    public long ServerTime { get; set; }
    public List<KeyModel> PublicKeys { get; set; }
}