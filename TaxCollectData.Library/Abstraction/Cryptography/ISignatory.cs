namespace TaxCollectData.Library.Abstraction.Cryptography;

public interface ISignatory
{
    string Sign(string text);
    string Sign(object data);
}