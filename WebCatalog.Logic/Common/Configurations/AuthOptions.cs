using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebCatalog.Logic.Common.Configurations;

public record AuthOptions
{
    public string Issuer { get; set; } = default!;

    public string Audience { get; set; } = default!;

    public string SecretKey { get; set; } = default!;

    public int ExpireTimeTokenMinutes { get; set; }

    public int ExpireTimeRefreshTokenDays { get; set; }

    public SymmetricSecurityKey SymmetricSecurityKey
    {
        get
        {
            if (string.IsNullOrWhiteSpace(SecretKey))
            {
                throw new NullReferenceException(nameof(SecretKey));
            }

            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        }
    }

    public int UserNameMinLength { get; set; }

    public int UserNameMaxLength { get; set; }

    public int EmailMaxLength { get; set; }
}