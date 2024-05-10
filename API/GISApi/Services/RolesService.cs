using AutoMapper;
using GISApi.Data;
using GISApi.Data.GlobalEntities;
using GISApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Services
{
    public interface IRolesService
    {
        Task<RoleViewModel> GetRoleById(string roleId);
        Task<bool> DeleteRole(string roleId);
        Task<bool> IsRoleAssignedToAnyUser(string roleId);


    }
    public class RolesService : IRolesService
    {
        private readonly IMapper _mapper;
        private GlobalDBContext _GlobalDBContext;
        private readonly ILogger<RolesService> _logger;

        public RolesService(ILogger<RolesService> logger, GlobalDBContext GlobalDBContext, IMapper mapper)
        {
            _mapper = mapper;
            _GlobalDBContext = GlobalDBContext;
            _logger = logger;
        }

        /// <summary>
        /// Check if role is assigned to any user
        /// </summary>
        /// <param name="roleId">string</param>
        /// <returns>bool</returns>
        public async Task<bool> IsRoleAssignedToAnyUser(string roleId)
        {
            try
            {
                return false;// await _GlobalDBContext.Aspnetuserroles.AnyAsync(x => x.RoleId == roleId);
            }
            catch (Exception ex)
            {
                _logger.LogError("[" + nameof(RolesService) + "." + nameof(IsRoleAssignedToAnyUser) + "]" + ex);
                throw ex;
            }
        }

        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="roleId">string</param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteRole(string roleId)
        {
            try
            {
                var pos = await _GlobalDBContext.AspNetRoles.Where(x => x.Id == roleId).FirstOrDefaultAsync();
                _GlobalDBContext.Remove(pos);
                await _GlobalDBContext.SaveChangesAsync();

                //var usrroles = await _GlobalDBContext.Aspnetuserroles.Where(x => x.RoleId == roleId).ToListAsync();
                //_GlobalDBContext.RemoveRange(usrroles);
                await _GlobalDBContext.SaveChangesAsync();             

                //List<AspNetRoleClaim> aspnetroleclaims = await _GlobalDBContext.AspNetRoleClaims.Where(x => x.RoleId == roleId).ToListAsync();
                //_GlobalDBContext.RemoveRange(aspnetroleclaims);
                //await _GlobalDBContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[" + nameof(RolesService) + "." + nameof(DeleteRole) + "]" + ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get role by id
        /// </summary>
        /// <param name="roleId">string</param>
        /// <returns>RoleViewModel</returns>
        public async Task<RoleViewModel> GetRoleById(string roleId)
        {
            try
            {
                RoleViewModel roleViewModel = new RoleViewModel();
                var role = await _GlobalDBContext.AspNetRoles.Where(x => x.Id == roleId).FirstOrDefaultAsync();
                roleViewModel.Id = role.Id;
                roleViewModel.Name = role.Name;
                return roleViewModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
