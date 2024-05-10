using AutoMapper;
using GISApi.Data;
using GISApi.Data.GlobalEntities;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Services
{
    public interface IInspectionTypeService
    {
        Task<InspectionType> AddEditInspectionType(InspectionType model);
        Task<List<InspectionType>> GetInspectionTypesList();
        Task<InspectionType> GetInspectionTypeById(int id);
        string DeleteInspectionTypeById(int id);
        Task<InspectionType> EditInspectionType(InspectionType model);
    }
    public class InspectionTypeService: IInspectionTypeService
    {
        private readonly IConfiguration _iconnectionstring;
        private readonly IMapper _mapper;
        private GlobalDBContext _GlobalDBContext;
        private readonly ILogger<InspectionService> _logger;
        private string connectionstring;

        public InspectionTypeService(IConfiguration iconnectionstring, ILogger<InspectionService> logger, GlobalDBContext GlobalDBContext, IMapper mapper)
        {
            _iconnectionstring = iconnectionstring;
            _mapper = mapper;
            _GlobalDBContext = GlobalDBContext;
            _logger = logger;
        }
        public async Task<InspectionType> AddEditInspectionType(InspectionType model)
        {
            try
            {
                InspectionType insp = new InspectionType();
                insp.Id = model.Id;
                insp.InspectionTypeName = model.InspectionTypeName;
                insp.IsActive = model.IsActive;
                var resuil = _GlobalDBContext.InspectionTypes.Add(insp);
                _GlobalDBContext.SaveChanges();
                return insp;

            }catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionTypeService] - AddEditInspectionType() Exception is : " + ex);
                return null;
            }
        }

        public async Task<InspectionType> EditInspectionType(InspectionType model)
        {
            try
            {
                var resultUpdate = await _GlobalDBContext.InspectionTypes.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

                resultUpdate.InspectionTypeName = model.InspectionTypeName;
                resultUpdate.IsActive = model.IsActive;

                var result = _GlobalDBContext.InspectionTypes.Update(resultUpdate);
                await _GlobalDBContext.SaveChangesAsync();
                
                return resultUpdate;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionTypeService] - AddEditInspectionType() Exception is : " + ex);
                return null;
            }
        }
        public async Task<List<InspectionType>> GetInspectionTypesList()
        {
            try
            {
                using (GlobalDBContext db = new GlobalDBContext())
                {
                    var inspectionTypelst = db.InspectionTypes.AsQueryable();
                    var result = await inspectionTypelst.Select(dataList =>
                                   new InspectionType
                                   {
                                       Id = dataList.Id,
                                       InspectionTypeName = dataList.InspectionTypeName,
                                       IsActive = dataList.IsActive

                                   }).OrderBy(x => x.InspectionTypeName).ToListAsync();
                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occured in [InspectionTypeService] - GetInspectionTypesList() Exception is : " + ex);
                return null;
            }

        }

        public async Task<InspectionType> GetInspectionTypeById(int id)
        {
            try
            {
                using (GlobalDBContext db = new GlobalDBContext())
                {
                    var result = await db.InspectionTypes.Where(c => c.Id == id).FirstOrDefaultAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionTypeService] - GetInspectionTypeById() Exception is : " + ex);
                return null;
            }

        }

        public string DeleteInspectionTypeById(int id)
        {
            try
            {
                using (GlobalDBContext db = new GlobalDBContext())
                {
                    var cmp = db.InspectionTypes.Where(c => c.Id == id).FirstOrDefault();
                    db.InspectionTypes.Remove(cmp);
                    db.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionTypeService] - DeleteInspectionTypeById() Exception is : " + ex);
                return "Fail";
            }
        }

    }
}
