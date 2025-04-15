namespace fitnessweb.Core.Helpers;
using Isopoh.Cryptography.Argon2;

public class PasswordHasher
{
    public static string Hash(string password)
    {
        var hash = Argon2.Hash(password);
        return hash;
    }

    public static bool Verify(string password, string hash)
    {
        return Argon2.Verify(hash, password);
    }
}