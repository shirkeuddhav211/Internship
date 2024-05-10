using GISApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GISApi.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity,string RoleId);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id, List<string> roles);

        Task<List<Claim>> GetUserPermissionsAsync(string roleName);

    }
}
