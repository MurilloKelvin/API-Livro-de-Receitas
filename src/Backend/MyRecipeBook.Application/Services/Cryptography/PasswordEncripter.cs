using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Cryptography;

public class PasswordEncripter
{
    private readonly string _salt;

    public PasswordEncripter(string salt) => _salt = salt; // construtor que recebe um salt para aumentar a segurança da senha
 

    // func para encriptar a senha usando SHA-512
    public string Encrypt(string password)
    {

        var newPass = $"{password}{_salt}"; // adiciona um salt para aumentar a segurança da senha
        
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