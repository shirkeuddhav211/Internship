using GISApi.Data;
using GISApi.Models;
using GISApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace GISApi.Auth
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRolesService _rolesService;
        private readonly IConfiguration _config;
        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions, RoleManager<IdentityRole> roleManager
            , IRolesService rolesService, IConfiguration config)
        {
            _roleManager = roleManager;
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
            _rolesService = rolesService;
            _config = config;
        }

        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, string RoleId)
        {
            var UTCDateTime = DateTime.UtcNow;
            var Iat = ToUnixEpochDate(UTCDateTime).ToString();
            var expiryDateTime = new DateTime();
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, Iat, ClaimValueTypes.Integer64),                  
                 identity.FindFirst(ClaimTypes.PrimarySid),
                 identity.FindFirst(ClaimTypes.Name)             
                 
        };
                                                                                                                                    
            var roles = identity.FindAll(ClaimTypes.Role);
            claims = claims.Concat(roles).ToArray();
            foreach (var role in roles)
            {
                var claimList = await GetUserPermissionsAsync(role.Value);
                claims = claims.Concat(claimList).ToArray();
            }

            //var userClaims = claims.Concat(userPermissions);
            expiryDateTime = DateTime.Now.AddHours(_config.GetValue<double>("Token:Lifetime"));
            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims.ToList(),
                notBefore: DateTime.Now,
                expires: expiryDateTime,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id, List<string> roles)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(ClaimTypes.PrimarySid, id),
                new Claim(ClaimTypes.Name, userName)               
            });
            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            return claimsIdentity;
        }

        public async Task<List<Claim>> GetUserPermissionsAsync(string roleName)
        {
            IdentityRole identityRole = await _roleManager.FindByNameAsync(roleName);
            var permissions = await _roleManager.GetClaimsAsync(identityRole);
            return permissions.ToList();
        }



        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
