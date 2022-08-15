using ApiClimate.Data;
using ApiClimate.Domain;
using ApiClimate.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiClimate.Services
{
    public class AuthenticationServices
    {
        private readonly ClimateDbContext dbClimate;
        private readonly IConfiguration configuration;

        public AuthenticationServices(ClimateDbContext climatecontext, IConfiguration iConfiguration)
        {
            dbClimate = climatecontext;          
            configuration = iConfiguration;
        }

        public async Task<AuthenticationResponse> GetToken(AuthenticationRequest modelRequest)
        {
            return await BuildToken(modelRequest);          

        }

        private async Task<AuthenticationResponse> BuildToken(AuthenticationRequest modelRequest)
        {
            AuthenticationResponse authenticationResponse = new AuthenticationResponse();
            try
            {
                var result = dbClimate.UserAplication.Where(x => x.Login == modelRequest.UserName && x.Password == modelRequest.Password).FirstOrDefault();

                if (result != null)
                {
                    var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.UniqueName, modelRequest.UserName),
                new Claim("agoraToken", "SemEnganacao"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    // tempo de expiração do token: 2 hora
                    var expiration = DateTime.UtcNow.AddHours(2);
                    var create = DateTime.UtcNow;
                    JwtSecurityToken token = new JwtSecurityToken(
                       issuer: null,
                       audience: null,
                       claims: claims,
                       expires: expiration,
                       signingCredentials: creds);

                    TokenAplicacao tokenAplicacao = new TokenAplicacao
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Created_at = create,
                        Updated_at = create,
                        Expired_at = expiration,
                        Active = true,
                        Id_UserAplication = result.Id
                    };

                    await dbClimate.TokenAplicacao.AddAsync(tokenAplicacao);
                    await dbClimate.SaveChangesAsync();

                    authenticationResponse.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    authenticationResponse.StatusCode = 200;

                    return authenticationResponse;
                }
                else
                {
                    authenticationResponse.Token = "Usuário não autenticado";
                    authenticationResponse.StatusCode = 401;

                    return authenticationResponse;
                }
            }
            catch (Exception ex)
            {
                authenticationResponse.Token = ex.ToString();
                authenticationResponse.StatusCode = 400;

                return authenticationResponse;               
            }

         
        }
    }
}
