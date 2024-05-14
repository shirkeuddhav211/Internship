using AutoMapper;
using GISApi.Data;
using GISApi.Data.GlobalEntities;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Services
{
    public interface INoticeService
    {
        Task<Notice> AddEditNotice(Notice model);
        Task<Notice> GetNoticeById(int id);
        Task<List<Notice>> GetNoticeList();
        string DeleteNoticeById(int id);
    }
    public class NoticeService : INoticeService
    {
        private readonly IConfiguration _iconnectionstring;
        private readonly IMapper _mapper;
        private GlobalDBContext _GlobalDBContext;
        private readonly ILogger<NoticeService> _logger;
        private string connectionstring;

        public NoticeService(IConfiguration iconnectionstring, ILogger<NoticeService> logger, GlobalDBContext GlobalDBContext, IMapper mapper)
        {
            _iconnectionstring = iconnectionstring;
            _mapper = mapper;
            _GlobalDBContext = GlobalDBContext;
            _logger = logger;
        }

        async Task<Notice> INoticeService.AddEditNotice(Notice model)
        {
            try
            {
                Notice notice = new Notice();
                notice.Id = model.Id;
                notice.Notice1  = model.Notice1;
                var result = _GlobalDBContext.Notices.Add(notice);
                _GlobalDBContext.SaveChanges();
                return notice;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [NoticeService] - AddEditNotice() Exception is : " + ex);
                return null;
            }
        }

        async Task<Notice> INoticeService.GetNoticeById(int id)
        {
            try
            {
                using (GlobalDBContext db = new GlobalDBContext())
                {
                    var result = await db.Notices.Where(c => c.Id == id).FirstOrDefaultAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [NoticeService] - GetNoticeeById() Exception is : " + ex);
                return null;
            }
        }

       async Task<List<Notice>> INoticeService.GetNoticeList()
        {
            try
            {
                using (GlobalDBContext db = new GlobalDBContext())
                {
                    var noticelst = db.Notices.AsQueryable();
                    var result = await noticelst.Select(dataList =>
                                   new Notice
                                   {
                                       Id = dataList.Id,
                                       Notice1  = dataList.Notice1 

                                   }).OrderBy(x => x.Notice1).ToListAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [NoticeService] - GetNoticeList() Exception is : " + ex);
                return null;
            }

        }

        public string DeleteNoticeById(int id)
        {
            try
            {
                using (GlobalDBContext db = new GlobalDBContext())
                {
                    var cmp = db.Notices.Where(c => c.Id == id).FirstOrDefault();
                    db.Notices .Remove(cmp);
                    db.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [NoticeService] - DeleteNoticeById() Exception is : " + ex);
                return "Fail";
            }
        }
    }
}
