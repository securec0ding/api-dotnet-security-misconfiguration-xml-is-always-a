using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Backend
{
    public static class JwtConfiguration
    {
        public const string SigningKey = "defaultsecretkey";
        public const string Audience = "Hunter2 BadSecretKey App";
        public const string Issuer = "Hunter2 BadSecretKey App";
        public const int ExpireSeconds = 864000;

        public static TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                ValidateAudience = true,
                ValidateIssuer = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SigningKey)),
                ValidateIssuerSigningKey = true,
            };
        }

        public static byte[] SigningKeyBytes
        {
            get
            {
                return Encoding.UTF8.GetBytes(SigningKey);
            }
        }
    }
}