using AutoMapper;
using GISApi.Data;
using GISApi.Data.GlobalEntities;
using GISApi.Models;
using GISApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GISApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionController : ControllerBase
    {
        private readonly ICommonService _commonService;

        private readonly ILogger<UsersController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        private IInspectionService _inspectionService;
        private GlobalDBContext _GlobalDBContext;
        public InspectionController(GlobalDBContext GlobalDBContext,
IInspectionService inspectionService, ILogger<UsersController> logger, RoleManager<IdentityRole> roleManager, ICommonService commonService,
            IConfiguration config, ApplicationDbContext appDbContext, IWebHostEnvironment hostingEnvironment, IMapper mapper)
        {
            _GlobalDBContext = GlobalDBContext;

            _logger = logger;
            _mapper = mapper;
            _config = config;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
            _webHostEnvironment = hostingEnvironment;
            _inspectionService = inspectionService; _commonService = commonService;

        }

        /// <summary>
        /// GetInspectionList
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetInspectionList")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IEnumerable<InspctionViewModel>> GetInspectionList(string fromDate, string toDate)
        {
            try
            {
                return await _inspectionService.GetInspectionList(fromDate, toDate);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionController] - GetInspectionList() Exception is : " + ex);
                return null;
            }
        }

        [HttpGet]
        [Route("EditInspectionackValue")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<ActionResult<Inspection>> EditInspectionackValue(string id, string ackvalue)
        {
            try
            {
                Inspection inspupdate =  await _inspectionService.EditInspectionackValue(id, ackvalue);
                return inspupdate;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionController] - EditInspectionackValue() Exception is : " + ex);
                return null;
            }
        }

        [HttpGet]
        [Route("EditInspectionRejectValue")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<ActionResult<Inspection>> EditInspectionRejectValue(string id, string rejectvalue, string rejectComments)
        {
            try
            {
                Inspection inspupdate = await _inspectionService.EditInspectionRejectValue(id, rejectvalue, rejectComments);
                return inspupdate;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionController] - EditInspectionRejectValue() Exception is : " + ex);
                return null;
            }
        }
        [HttpGet]
        [Route("DeleteInspectionById/{id}")]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult DeleteInspectionById(string id)
        {
            try
            {

                var inspection = _inspectionService.DeleteInspectionById(Convert.ToInt32(id));
                return Ok(new { statusText = inspection });

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionController] - DeleteInspectionById(id) Exception is : " + ex);
                return Ok(new { statusText = "Fail" });
            }
        }


        [HttpPost]
        [Route("AddInspectionDetail")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<ActionResult> AddInspectionDetail()
        {
            try
            {
                InspctionViewModel inspectiondDetail = JsonConvert.DeserializeObject<InspctionViewModel>(HttpContext.Request.Form["InspectionDetail"]);
                var result = await _inspectionService.AddInspectionDetail(inspectiondDetail);

                var insp = _GlobalDBContext.Inspections.Where(x => x.InspectionOrder == inspectiondDetail.InspectionOrder).FirstOrDefault();
                return Ok(insp.Id.ToString());

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionController] - GetInspectionList() Exception is : " + ex);
                return null;
            }
        }

        [HttpGet]
        [Route("GetInspectionDetailsById")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<ActionResult> GetInspectionDetailsById(int inspectionId)
        {
            try
            {
                
                InspctionViewModel inspectionRecordDetail = new InspctionViewModel();

                inspectionRecordDetail = await _inspectionService.GetInspectionDetailsById(inspectionId);
                return Ok(inspectionRecordDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionController] - GetInspectionList() Exception is : " + ex);
                return null;
            }
        }

        [HttpGet]
        [Route("GetInspectionListWithoutDate")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> GetInspectionListWithoutDate()
        {
            return Ok(await _inspectionService.GetInspectionListWithoutDate());
        }

        
        [HttpGet]
        [Route("UpdateInspectionInspector")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<ActionResult<Inspection>> UpdateInspectionInspector(int id, string inspector)
        {
            try
            {
                Inspection inspupdate = await _inspectionService.UpdateInspectionInspector(id, inspector);
                return inspupdate;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionController] - EditInspectionRejectValue() Exception is : " + ex);
                return null;
            }
        }

        [HttpGet]
        [Route("UpdateInspectionTime")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<ActionResult<Inspection>> UpdateInspectionTime(int id, string time)
        {
            try
            {
                Inspection inspupdate = await _inspectionService.UpdateInspectionTime(id, time);
                return inspupdate;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionController] - EditInspectionRejectValue() Exception is : " + ex);
                return null;
            }
        }

        [HttpGet]
        [Route("UpdateInspectionDate")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<ActionResult<Inspection>> UpdateInspectionDate(int id, string date)
        {
            try
            {
                Inspection inspupdate = await _inspectionService.UpdateInspectionDate(id,  date);
                return inspupdate;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionController] - EditInspectionRejectValue() Exception is : " + ex);
                return null;
            }
        }
    }
}
