using AutoMapper;
using GISApi.Data;
using GISApi.Data.GlobalEntities;
using GISApi.Models;
using GISApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;

namespace GISApi.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class InspectionTypeController : ControllerBase
    {
        private readonly ICommonService _commonService;

        private readonly ILogger<UsersController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        private IInspectionTypeService _inspectionTypeService;
        private GlobalDBContext _GlobalDBContext;

        public InspectionTypeController(GlobalDBContext GlobalDBContext, IInspectionTypeService inspectionTypeService, ILogger<UsersController> logger, RoleManager<IdentityRole> roleManager, ICommonService commonService,
            IConfiguration config, ApplicationDbContext appDbContext, IWebHostEnvironment hostingEnvironment, IMapper mapper)
        {
            _GlobalDBContext = GlobalDBContext;

            _logger = logger;
            _mapper = mapper;
            _config = config;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
            _webHostEnvironment = hostingEnvironment;
            _inspectionTypeService = inspectionTypeService; _commonService = commonService;

        }

        [HttpPost]
        [Route("AddEditInspectionType")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<InspectionType> AddEditInspectionType()
        {
            try
            {
                InspectionType inspectionTypes = JsonConvert.DeserializeObject<InspectionType>(HttpContext.Request.Form["inspectiontype"]);

                return await _inspectionTypeService.AddEditInspectionType(inspectionTypes);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionTypeController] - AddEditInspectionType(inspectionTypes) Exception is : " + ex);
                return null;
            }

        }

        [HttpPut("{id}")]

        public async Task<ActionResult> Put(string id, [FromBody] InspectionType model)
        {
            try
            {
                InspectionType editModel = null;

                editModel = await _inspectionTypeService.EditInspectionType(model);
                return Ok(editModel);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occured in [InspectionTypeController] - AddEditInspectionType(inspectionTypes) Exception is : " + ex);
                return null;
            }
        }


        [HttpGet]
        [Route("GetInspectionTypesList")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> GetInspectionTypesList()
        {
            return Ok(await _inspectionTypeService.GetInspectionTypesList());
        }


        [HttpGet]
        [Route("GetInspectionTypeById/{id}")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> GetInspectionTypeById(int id)
        {

            return Ok(await _inspectionTypeService.GetInspectionTypeById(id));

        }

        [HttpGet]
        [Route("DeleteInspectionTypeById/{id}")]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult DeleteInspectionTypeById(int id)
        {
            try
            {

               var insptype = _inspectionTypeService.DeleteInspectionTypeById(id);
               return Ok(new { statusText = insptype });
               
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionTypeController] - DeleteInspectionTypeById(id) Exception is : " + ex);
                return Ok(new { statusText = "Fail" });
            }

        }
        
    }
}
