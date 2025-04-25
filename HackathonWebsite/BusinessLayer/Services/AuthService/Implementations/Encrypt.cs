using HackathonWebsite.BusinessLayer.Services.AuthService.Abstractions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace HackathonWebsite.BusinessLayer.Services.AuthService.Implementations;

public class Encrypt : IEncrypt
{
    public string HashPassword(string password, string salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            System.Text.Encoding.ASCII.GetBytes(salt.ToString()),
            KeyDerivationPrf.HMACSHA512,
            100_000,
            64));
    }
}