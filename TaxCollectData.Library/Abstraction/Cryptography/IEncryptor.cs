namespace TaxCollectData.Library.Abstraction.Cryptography;

public interface IEncryptor
{
    string Encrypt(string text);
}