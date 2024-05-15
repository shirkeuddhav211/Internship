using AutoMapper;
using GISApi.Data;
using GISApi.Data.GlobalEntities;
using GISApi.Models;
using GISApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using Microsoft.AspNetCore.Cors;

namespace GISApi.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class NoticeController : ControllerBase
    {  

        private readonly ICommonService _commonService;

        private readonly ILogger<UsersController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        private INoticeService _noticeService;
        private GlobalDBContext _GlobalDBContext;

        public NoticeController(GlobalDBContext GlobalDBContext, INoticeService noticeService, ILogger<UsersController> logger, RoleManager<IdentityRole> roleManager, ICommonService commonService,
            IConfiguration config, ApplicationDbContext appDbContext, IWebHostEnvironment hostingEnvironment, IMapper mapper)
        {
            _GlobalDBContext = GlobalDBContext;

            _logger = logger;
            _mapper = mapper;
            _config = config;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
            _webHostEnvironment = hostingEnvironment;
            _noticeService = noticeService; _commonService = commonService;

        }

        [HttpGet]
        [Route("GetNoticeById/{id}")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> GetNoticeById(int id)
        {

            return Ok(await _noticeService.GetNoticeById(id));

        }

        [HttpPost]
        [Route("AddEditNotice")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<Notice> AddEditNotice()
        {
            try
            {
                Notice notice = JsonConvert.DeserializeObject<Notice>(HttpContext.Request.Form["notice"]);

                return await _noticeService.AddEditNotice(notice);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [NoticeController] - AddEditNotice(inspectionTypes) Exception is : " + ex);
                return null;
            }

        }

        [HttpPut("{id}")]

        public async Task<ActionResult> Put(string id, [FromBody] Notice model)
        {
            try
            {
                Notice editModel = null;

                editModel = await _noticeService.EditNotice(model);
                return Ok(editModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [NoticeController] - EditNotice(notice) Exception is : " + ex);
                return null;
            }
        }


        [HttpGet]
        [Route("GetNoticeList")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> GetNoticesList()
        {
            return Ok(await _noticeService.GetNoticeList());
        }

        [HttpGet]
        [Route("DeleteNoticeById/{id}")]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult DeleteNoticeById(int id)
        {
            try
            {

                var notice = _noticeService.DeleteNoticeById(id);
                return Ok(new { statusText = notice });

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [NoticeController] - DeleteNoticeById(id) Exception is : " + ex);
                return Ok(new { statusText = "Fail" });
            }

        }

    }
}
