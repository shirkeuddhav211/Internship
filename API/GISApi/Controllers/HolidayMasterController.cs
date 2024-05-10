using AutoMapper;
using GISApi.Data;
using GISApi.Data.GlobalEntities;
using GISApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GISApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayMasterController : ControllerBase
    {
        private readonly ICommonService _commonService;

        private readonly ILogger<UsersController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        private IHolidayMasterService _holidayMasterService;
        private GlobalDBContext _GlobalDBContext;

        public HolidayMasterController(GlobalDBContext GlobalDBContext, IHolidayMasterService holidayMasterService, ILogger<UsersController> logger, RoleManager<IdentityRole> roleManager, ICommonService commonService,
            IConfiguration config, ApplicationDbContext appDbContext, IWebHostEnvironment hostingEnvironment, IMapper mapper)
        {
            _GlobalDBContext = GlobalDBContext;

            _logger = logger;
            _mapper = mapper;
            _config = config;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
            _webHostEnvironment = hostingEnvironment;
            _holidayMasterService = holidayMasterService; _commonService = commonService;

        }
        // GET: api/<HolidayMasterController>
        [HttpGet]
        [Route("GetHolidayList")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> GetHolidayList()
        {
            return Ok(await _holidayMasterService.GetHolidayList());
        }


        // GET api/<HolidayMasterController>/5
        [HttpGet]
        [Route("GetHolidayById/{id}")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> GetHolidayById(int id)
        {

            return Ok(await _holidayMasterService.GetHolidayById(id));

        }

        // DELETE api/<HolidayMasterController>/5
        [HttpGet]
        [Route("DeleteHolidayById/{id}")]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult DeleteInspectionTypeById(int id)
        {
            try
            {

                var insptype = _holidayMasterService.DeleteHolidayById(id);
                return Ok(new { statusText = insptype });

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [HolidayMasterController] - DeleteHolidayById(id) Exception is : " + ex);
                return Ok(new { statusText = "Fail" });
            }

        }

        // POST api/<HolidayMasterController>        
        [HttpPost]
        [Route("AddEditHoliday")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<HolidayMaster> AddEditHoliday()
        {
            try
            {
                HolidayMaster holidayMaster = JsonConvert.DeserializeObject<HolidayMaster>(HttpContext.Request.Form["holiday"]);

                return await _holidayMasterService.AddEditHoliday(holidayMaster);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [HolidayMasterController] - AddEditHoliday(holiday) Exception is : " + ex);
                return null;
            }

        }

        // PUT api/<HolidayMasterController>/5
       
        [HttpPut("{id}")]

        public async Task<ActionResult> Put(string id, [FromBody] HolidayMaster model)
        {
            try
            {
                HolidayMaster editModel = null;

                editModel = await _holidayMasterService.EditHoliday(model);
                return Ok(editModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [HolidayMasterController] - AddEditHoliday(inspectionTypes) Exception is : " + ex);
                return null;
            }
        }

       
       
    }
}
