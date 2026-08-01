public class PasswordHasher
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    public static bool VerifyPasswordHash(string password, byte[] storeHash, byte[] storeSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(storeSalt);
        var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return ComputeHash.SequenceEqual(storeHash);
    }
}