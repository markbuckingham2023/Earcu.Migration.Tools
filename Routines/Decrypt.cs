using System.Security.Cryptography;
using System.Text;

namespace Routines
{
    public class Decrypt
    {
        public string Exec(string cipherText)
        {
            using (var rijAlg = new RijndaelManaged())
            {
                byte[] salt = Encoding.UTF8.GetBytes("saltydogcrispsourlovely");

                var key = new Rfc2898DeriveBytes(new Routines.Config().ReadValue("Secret"), salt);
                rijAlg.Key = key.GetBytes(rijAlg.KeySize / 8);
                rijAlg.IV = key.GetBytes(rijAlg.BlockSize / 8);

                using (var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV))
                using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }


    }
}
