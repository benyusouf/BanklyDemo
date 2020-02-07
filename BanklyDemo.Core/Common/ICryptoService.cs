namespace BanklyDemo.Core.Common
{
    public interface ICryptoService
    {
        string Hash(string text, string salt = null, int iterations = 1);

        string GenerateSalt(int maxLenght);

        string CreateUniqueKey(int length = 32);

        string Encrypt(string plainText, string key);

        string Decrypt(string cipherText, string key);
    }
}
