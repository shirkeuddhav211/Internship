using GISApi.Models;
using GISApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GISApi.Auth
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler()
        {

        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;
            }
            var requiredPermissions = requirement.Permission.Split(',');
            
            //if user has role BackendAdmin then bypass the auth
            var roles = context.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x=>x.Value).ToList();
            if(roles.Contains("BackendAdmin") || roles.Contains("SuperAdmin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            
            //does user have any of the required permissions?
            var hasPermission = context.User.Claims.Any(x => x.Type == CustomClaimTypes.Permission &&
                                                       requiredPermissions.Any(permission => permission.Trim() == x.Value.Trim()) &&
                                                       x.Issuer == "ERPWebApi");
            if (hasPermission)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;

        }
    }
}
