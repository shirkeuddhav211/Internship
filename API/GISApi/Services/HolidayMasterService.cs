using AutoMapper;
using GISApi.Data;
using GISApi.Data.GlobalEntities;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Services
{
    public interface IHolidayMasterService
    {
        Task<HolidayMaster> AddEditHoliday(HolidayMaster model);
        Task<HolidayMaster> EditHoliday(HolidayMaster model);
        Task<List<HolidayMaster>> GetHolidayList();
        Task<HolidayMaster> GetHolidayById(int id);
        string DeleteHolidayById(int id);
    }
    public class HolidayMasterService : IHolidayMasterService
    {
        private readonly IConfiguration _iconnectionstring;
        private readonly IMapper _mapper;
        private GlobalDBContext _GlobalDBContext;
        private readonly ILogger<HolidayMasterService> _logger;
        private string connectionstring;

        public HolidayMasterService(IConfiguration iconnectionstring, ILogger<HolidayMasterService> logger, GlobalDBContext GlobalDBContext, IMapper mapper)
        {
            _iconnectionstring = iconnectionstring;
            _mapper = mapper;
            _GlobalDBContext = GlobalDBContext;
            _logger = logger;
        }

        public async Task<HolidayMaster> AddEditHoliday(HolidayMaster model)
        {
            try
            {
                HolidayMaster holiday = new HolidayMaster();
                holiday.Id = model.Id;
                holiday.HolidayDate = model.HolidayDate;
                holiday.Description = model.Description;
                var resuil = _GlobalDBContext.HolidayMasters.Add(holiday);
                _GlobalDBContext.SaveChanges();
                return holiday;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [HolidayMasterService] - AddEditHoliday() Exception is : " + ex);
                return null;
            }
        }

        public async Task<HolidayMaster> EditHoliday(HolidayMaster model)
        {
            try
            {
                var resultUpdate = await _GlobalDBContext.HolidayMasters.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

                resultUpdate.HolidayDate = model.HolidayDate;
                resultUpdate.Description = model.Description;

                var result = _GlobalDBContext.HolidayMasters.Update(resultUpdate);
                await _GlobalDBContext.SaveChangesAsync();

                return resultUpdate;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionTypeService] - EditHoliday() Exception is : " + ex);
                return null;
            }
        }
        public async Task<List<HolidayMaster>> GetHolidayList()
        {
            try
            {
                using (GlobalDBContext db = new GlobalDBContext())
                {
                    var holidays = db.HolidayMasters.AsQueryable();
                    var result = await holidays.Select(dataList =>
                                   new HolidayMaster
                                   {
                                       Id = dataList.Id,
                                       HolidayDate = dataList.HolidayDate,
                                       Description = dataList.Description

                                   }).OrderBy(x => x.HolidayDate).ToListAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionTypeService] - GetHolidayList() Exception is : " + ex);
                return null;
            }

        }

        public async Task<HolidayMaster> GetHolidayById(int id)
        {
            try
            {
                using (GlobalDBContext db = new GlobalDBContext())
                {
                    var result = await db.HolidayMasters.Where(c => c.Id == id).FirstOrDefaultAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionTypeService] - GetHolidayList() Exception is : " + ex);
                return null;
            }

        }

        public string DeleteHolidayById(int id)
        {
            try
            {
                using (GlobalDBContext db = new GlobalDBContext())
                {
                    var cmp = db.HolidayMasters.Where(c => c.Id == id).FirstOrDefault();
                    db.HolidayMasters.Remove(cmp);
                    db.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionTypeService] - DeleteHolidayById() Exception is : " + ex);
                return "Fail";
            }
        }
    }
}
