using AutoMapper;
using GISApi.Data;
using GISApi.Helpers;
using GISApi.Models;
using GISApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using DocumentFormat.OpenXml.InkML;
using GISApi.Data.GlobalEntities;

namespace GISApi.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
   
    public class RoleController : ControllerBase
    {
        private readonly ICommonService _commonService;

        private readonly ILogger<UsersController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        private IRolesService _roleService;
        private GlobalDBContext _GlobalDBContext;
        public RoleController(GlobalDBContext GlobalDBContext,
IRolesService roleService, ILogger<UsersController> logger, RoleManager<IdentityRole> roleManager, ICommonService commonService,
            IConfiguration config, ApplicationDbContext appDbContext, IWebHostEnvironment hostingEnvironment, IMapper mapper)
        {
            _GlobalDBContext = GlobalDBContext;

            _logger = logger;
            _mapper = mapper;
            _config = config;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
            _webHostEnvironment = hostingEnvironment;
            _roleService = roleService; _commonService = commonService;

        }

        /// <summary>
        /// Get User role list
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //[AllowAnonymous]
        //[EnableCors("AllowSpecificOrigin")]

        ////[Authorize(Policy = "Permissions.Site Admin.User.ReadOnly,Permissions.Site Admin.User.AddUpdateDelete")]
        //public async Task<ActionResult<List<RoleViewModel>>> GetAllRolesList()
        //{
        //    try
        //    {
        //        string roleName = User.Claims.Single(c => c.Type == ClaimTypes.Role).Value;
        //        if(roleName== "Superadmin")
        //        {
        //            List<RoleViewModel> roles = await _roleManager.Roles.Where(x => x.Name != "Superadmin").Select(x => new RoleViewModel { Name = x.Name, Id = x.Id }).ToListAsync();
        //            return Ok(roles);
        //        }
        //        else
        //        {
        //            List<RoleViewModel> roles = await _roleManager.Roles.Where(x => x.Name != "Superadmin").Select(x => new RoleViewModel { Name = x.Name, Id = x.Id }).ToListAsync();
        //            return Ok(roles);
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("[" + nameof(UsersController) + "." + nameof(GetAllRolesList) + "]" + ex);
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        /// <summary>
        /// Get users by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User model</returns>
        [HttpGet("{id}")]
        //[Authorize(Policy = "Permissions.Site Admin.User.ReadOnly,Permissions.Site Admin.User.AddUpdateDelete")]
        public async Task<ActionResult<RoleViewModel>> Get(string id)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                string userId = User.Claims.Single(c => c.Type == ClaimTypes.PrimarySid).Value;
                if (!_commonService.ValidateToken(token, userId))
                {
                    ModelState.AddModelError("ERROR", "Someone else is logged in with this UserID.");
                    return StatusCode(StatusCodes.Status406NotAcceptable, new CustomBadRequest(ModelState));
                }



                RoleViewModel user = await _roleService.GetRoleById(id);
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("[" + nameof(UsersController) + "." + nameof(Get) + "]" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }        

        /// <summary>
        /// Create role
        /// </summary>
        /// <param name="roleModel"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Policy = "Permissions.Site Admin.User.AddUpdateDelete")]
        public async Task<IActionResult> Post(RoleViewModel roleModel)
        {
            try
            {

                if (roleModel.Name == "SuperAdmin")
                {
                    ModelState.AddModelError("Error", "Role name is already in use, please contact administration.");
                    return BadRequest(new CustomBadRequest(ModelState));
                }
                ApplicationRole role = new ApplicationRole();
                Guid id = Guid.NewGuid();
                role.Id = id.ToString();
                role.Name = roleModel.Name;
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("Error", result.Errors.FirstOrDefault().Description);
                    return BadRequest(new CustomBadRequest(ModelState));
                }
                var roleInfo = await _GlobalDBContext.AspNetRoles.Where(x => x.Id == role.Id).FirstOrDefaultAsync();
                await _GlobalDBContext.SaveChangesAsync();
                return Ok();        
            }
            catch (Exception ex)
            {

                return BadRequest();
            }

        }

        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="id">Role Id</param>
        /// <returns></returns>
        [HttpGet("delete/{id}")]
        //[Authorize(Policy = "Permissions.Site Admin.User.AddUpdateDelete")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                string userId = User.Claims.Single(c => c.Type == ClaimTypes.PrimarySid).Value;
                if (!_commonService.ValidateToken(token, userId))
                {
                    ModelState.AddModelError("ERROR", "Someone else is logged in with this UserID.");
                    return StatusCode(StatusCodes.Status406NotAcceptable, new CustomBadRequest(ModelState));
                }


                var role = await _roleService.GetRoleById(id);
                if (role == null)
                {
                    return NotFound(id);
                }

                //aspnetuserroles
                if (await _roleService.IsRoleAssignedToAnyUser(id))
                {
                    ModelState.AddModelError("ERROR", $"Cannot delete role ({role.Name}), as other users are assigned to this role.");
                    return BadRequest(new CustomBadRequest(ModelState));
                }

                var resultPosition = await _roleService.DeleteRole(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("[" + nameof(RoleController) + "." + nameof(Delete) + "]" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Edit role
        /// </summary>
        /// <param name="model">AddEditUserViewModel</param>
        /// <returns>RoleViewModel</returns>
        [HttpPut("{id}")]
        //[Authorize(Policy = "Permissions.Site Admin.User.AddUpdateDelete")]
        public async Task<ActionResult<OperationResult<RoleViewModel>>> Put(string id, [FromBody] RoleViewModel model)
        {
            
            if (id != model.Id)
            {
                return BadRequest();
            }
            if (model.Name == "BackendAdmin" || model.Name == "SuperAdmin")
            {
                ModelState.AddModelError("Error", "Role name is already in use, please contact administration.");
                return BadRequest(new CustomBadRequest(ModelState));
            }


            var rolesInDB = await _GlobalDBContext.AspNetRoles.Where(x => x.Name.ToUpper() == model.Name.ToUpper() && x.Id != id).ToListAsync();

            if (rolesInDB != null)
            {
                if (rolesInDB.Count == 0)
                {
                    var roleInfo = await _GlobalDBContext.AspNetRoles.Where(x => x.Id == id).FirstOrDefaultAsync();
                    roleInfo.Name = model.Name;
                    // roleInfo.NormalizedName = model.Name.ToUpper();                    
                    await _GlobalDBContext.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    ModelState.AddModelError("Error", "Role with same name exsists");
                    return BadRequest(new CustomBadRequest(ModelState));

                }
            }
            else
            {
                ModelState.AddModelError("Error", "Role with same name exsists");
                return BadRequest(new CustomBadRequest(ModelState));

            }

            //if (!IsModelValid(model))
            //{
            //    return BadRequest(new CustomBadRequest(ModelState));
            //}

        }


        [HttpGet]
        [Route("GetAllRolesList")]
        [EnableCors("AllowSpecificOrigin")]
        
        public IEnumerable<AspNetRole> GetAllRolesList()
        {
            try
            {
                List<AspNetRole> roles = _GlobalDBContext.AspNetRoles.ToList();
                return roles.Where(x => x.Name != "SuperAdmin").OrderByDescending(x=>x.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [AccountController] - GetAllRoles() Exception is : " + ex);
                return null;
            }
        }
    }
}
