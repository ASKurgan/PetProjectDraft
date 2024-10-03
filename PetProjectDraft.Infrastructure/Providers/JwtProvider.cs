using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using PetProjectDraft.Application.Interfaces.Providers;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.Entities;
using PetProjectDraft.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Infrastructure.Providers
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public Result<string> Generate(User user)
        {
            var jwtHandler = new JsonWebTokenHandler();

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            var permissionClaims = user.Role.Permissions
                .Select(p => new Claim(Constants.Constants.Authentication.Permissions, p));

            var claims = permissionClaims.Concat(
            [
                new(Constants.Constants.Authentication.UserId, user.Id.ToString()),
                new(Constants.Constants.Authentication.Role, user.Role.Name)
            ]);


            // tokenDescriptor - описание нашего токена
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new(claims), // набор claim-ов, который содержит в себе пользователь в системе. 
                                       //Subject - содержит набор ClaimsIdentity. ClaimsIdentity - identity - личность
                SigningCredentials = new(symmetricKey, SecurityAlgorithms.HmacSha256), // SigningCredentials необходим для
                                                                                       // расшифровки нашего токена и тд 
                Expires = DateTime.UtcNow.AddHours(_jwtOptions.Expires)
            };

            var token = jwtHandler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}
