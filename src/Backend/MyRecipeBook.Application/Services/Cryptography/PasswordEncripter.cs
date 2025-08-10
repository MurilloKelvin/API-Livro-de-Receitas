using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Cryptography;

public class PasswordEncripter
{
    // func para encriptar a senha usando SHA-512
    public string Encrypt(string password)
    {
        var salt = "JQW!@";  // string aleatoria
        
        var newPass = $"{password}{salt}"; // adiciona um salt para aumentar a segurança da senha
        
        var bytes = Encoding.UTF8.GetBytes(newPass);
        var hashBytes = SHA512.HashData(bytes);
        
        return StringBytes(hashBytes);
    }

    public static string StringBytes(byte[] bytes) // converte array de bytes para string hexadecimal
    {
        var sb = new StringBuilder();
        foreach (var b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }
}