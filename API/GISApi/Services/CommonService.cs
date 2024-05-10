using AutoMapper;
using GISApi.Data;

namespace GISApi.Services
{
    public interface ICommonService
    {
        bool ValidateToken(string token, string userid);
    }
    public class CommonService : ICommonService
    {
        private readonly IMapper _mapper;
        private GlobalDBContext _GlobalDBContext;
        private readonly ILogger<CommonService> _logger;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public CommonService(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, ILogger<CommonService> logger, GlobalDBContext GlobalDBContext, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _mapper = mapper;
            _GlobalDBContext = GlobalDBContext;
            _logger = logger;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
            _hostingEnvironment = hostingEnvironment;


        }
        public bool ValidateToken(string token, string userid)
        {
            try
            {
                var user = _GlobalDBContext.AspNetUsers.Where(x => x.Id == userid).FirstOrDefault();
              
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[" + nameof(CommonService) + "." + nameof(ValidateToken) + "]" + ex);

                throw;
            }
            throw new NotImplementedException();
        }
    }
}
