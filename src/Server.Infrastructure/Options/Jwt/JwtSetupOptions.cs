using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Server.Infrastructure.Options.Jwt;

internal sealed class JwtSetupOptions(
    IOptions<JwtOptions> jwtOptions) : IPostConfigureOptions<JwtBearerOptions>
{
    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));

        options.TokenValidationParameters.ValidateIssuer = true;
        options.TokenValidationParameters.ValidateAudience = true;
        options.TokenValidationParameters.ValidateLifetime = true;
        options.TokenValidationParameters.ValidIssuer = jwtOptions.Value.Issuer;
        options.TokenValidationParameters.ValidAudience = jwtOptions.Value.Audience;
        options.TokenValidationParameters.IssuerSigningKey = securityKey;
    }
}
