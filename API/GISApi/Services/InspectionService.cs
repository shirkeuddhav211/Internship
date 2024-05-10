using AutoMapper;
using GISApi.Data;
using GISApi.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using GISApi.Data.GlobalEntities;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Services
{
    public interface IInspectionService
    {
        Task<IEnumerable<InspctionViewModel>> GetInspectionList(string fromDate, string toDate);
        Task<IEnumerable<string>> AddInspectionDetail(InspctionViewModel inspectionRecordDetail);
        string DeleteInspectionById(int id);
        Task<InspctionViewModel> GetInspectionDetailsById(int incidentId);
        Task<List<Inspection>> GetInspectionListWithoutDate();
        Task<Inspection> EditInspectionackValue(string id, string ackvalue);
        Task<Inspection> EditInspectionRejectValue(string id, string rejectValue, string rejectComment);
        Task<Inspection> UpdateInspectionInspector(int id, string inspector);
        Task<Inspection> UpdateInspectionTime(int id, string time);
        Task<Inspection> UpdateInspectionDate(int id, string date);

    }
    public class InspectionService : IInspectionService
    {
        private readonly IConfiguration _iconnectionstring;
        private readonly IMapper _mapper;
        private GlobalDBContext _GlobalDBContext;
        private readonly ILogger<InspectionService> _logger;
        private string connectionstring;

        public InspectionService(IConfiguration iconnectionstring, ILogger<InspectionService> logger, GlobalDBContext GlobalDBContext, IMapper mapper)
        {
            _iconnectionstring = iconnectionstring;
            _mapper = mapper;
            _GlobalDBContext = GlobalDBContext;
            _logger = logger;
            connectionstring = _iconnectionstring["ConnectionStrings:DefaultConnection"];
        }

        public async Task<IEnumerable<InspctionViewModel>> GetInspectionList(string fromDate, string toDate)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(connectionstring))
                {
                    await sqlConnection.OpenAsync();
                    var param = new DynamicParameters();
                    param.Add("@fromDate", fromDate);
                    param.Add("@toDate", toDate);
                    var inspectionList = await sqlConnection.QueryAsync<InspctionViewModel>("GetInspectionList", param,
                    commandType: CommandType.StoredProcedure);


                    return inspectionList;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionService] - GetInspectionList() Exception is : " + ex);
                return null;
            }
        }

        public async Task<List<Inspection>> GetInspectionListWithoutDate()
        {
            try
            {
                using (GlobalDBContext db = new GlobalDBContext())
                {
                    var inspectionTypelst = db.Inspections.AsQueryable();
                    var result = await inspectionTypelst.Select(dataList =>
                                   new Inspection
                                   {
                                       Id = dataList.Id,
                                       FirstName = dataList.FirstName,
                                       LastName = dataList.LastName,
                                       InspectionOrder = dataList.InspectionOrder,
                                       InspectionDate = dataList.InspectionDate,
                                       AddressLine1 = dataList.AddressLine1,
                                       AddressLine2 = dataList.AddressLine2,
                                       State = dataList.State,
                                       City = dataList.City,
                                       InspectionType1 = dataList.InspectionType1,
                                       InspectionType2 = dataList.InspectionType2,
                                       InspectionType3 = dataList.InspectionType3,
                                       InspectionType4 = dataList.InspectionType4,
                                       Status1 = dataList.Status1,
                                       Status2 = dataList.Status2,
                                       Status3 = dataList.Status3,
                                       Status4 = dataList.Status4,
                                       PermitNo = dataList.PermitNo,
                                       InspectorName = dataList.InspectorName,
                                       Acknowledge = dataList.Acknowledge,
                                       RejectComments = dataList.RejectComments,
                                       IsRejected = dataList.IsRejected,
                                       AmPm = dataList.AmPm,

                                   }).OrderBy(x => x.Id).ToListAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionService] - GetInspectionTypesList() Exception is : " + ex);
                return null;
            }

        }


        public async Task<Inspection> EditInspectionackValue(string id, string ackvalue )
        {
            try
            {
                var resultUpdate = await _GlobalDBContext.Inspections.Where(x => x.Id == Convert.ToInt32(id)).FirstOrDefaultAsync();

                resultUpdate.Acknowledge = Convert.ToBoolean(ackvalue);
                resultUpdate.InspectionStatus = "Acknowledge";

                var result = _GlobalDBContext.Inspections.Update(resultUpdate);
                await _GlobalDBContext.SaveChangesAsync();

                return resultUpdate;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionService] - EditInspectionackValue() Exception is : " + ex);
                return null;
            }
        }

        public async Task<Inspection> EditInspectionRejectValue(string id, string rejectValue, string rejectComment)
        {
            try
            {
                var resultUpdate = await _GlobalDBContext.Inspections.Where(x => x.Id == Convert.ToInt32(id)).FirstOrDefaultAsync();

                resultUpdate.IsRejected = Convert.ToBoolean(rejectValue);
                resultUpdate.RejectComments = rejectComment;
                resultUpdate.InspectionStatus = "Rejected";


                var result = _GlobalDBContext.Inspections.Update(resultUpdate);
                await _GlobalDBContext.SaveChangesAsync();

                return resultUpdate;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionService] - EditInspectionackValue() Exception is : " + ex);
                return null;
            }
        }
        public async Task<IEnumerable<string>> AddInspectionDetail(InspctionViewModel inspectionRecordDetail)
        {
            try
            {
                using (GlobalDBContext db = new GlobalDBContext())
                {
                    InspectionNumberGenerator inspNumberGenerator = db.InspectionNumberGenerators.FirstOrDefault();
                    if (inspectionRecordDetail.InspectionOrder == 0 || inspectionRecordDetail.InspectionOrder == null)
                    {
                        inspectionRecordDetail.InspectionOrder = inspNumberGenerator.InspetionNextId;
                        inspNumberGenerator.InspetionNextId = inspNumberGenerator.InspetionNextId + 1;
                        db.SaveChanges();
                    }
                }

                var createdDateForInspection = "";
                using (GlobalDBContext db = new GlobalDBContext())
                {
                    var cd = db.Inspections.Where(x => x.InspectionOrder == inspectionRecordDetail.InspectionOrder).FirstOrDefault();
                    if (cd == null)
                    {
                        createdDateForInspection = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    }
                    else
                    {
                        createdDateForInspection = cd.CreatedDate.ToString();
                    }
                }

                using (var sqlConnection = new SqlConnection(connectionstring))
                {
                    await sqlConnection.OpenAsync();
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", inspectionRecordDetail.Id);
                    parameters.Add("@UserId", inspectionRecordDetail.UserId);
                    parameters.Add("@AddressLine1 ", inspectionRecordDetail.AddressLine1);
                    parameters.Add("@AddressLine2", inspectionRecordDetail.AddressLine2);
                    parameters.Add("@City", inspectionRecordDetail.City);
                    parameters.Add("@State", inspectionRecordDetail.State);
                    parameters.Add("@Zip", inspectionRecordDetail.Zip);
                    parameters.Add("@PhoneNumber", inspectionRecordDetail.PhoneNumber);
                    parameters.Add("@Alias", inspectionRecordDetail.Alias);
                    parameters.Add("@InspectionType1", inspectionRecordDetail.InspectionType1);
                    parameters.Add("@InspectionType2", inspectionRecordDetail.InspectionType2);
                    parameters.Add("@InspectionType3", inspectionRecordDetail.InspectionType3);
                    parameters.Add("@InspectionType4", inspectionRecordDetail.InspectionType4);
                    parameters.Add("@Status1", inspectionRecordDetail.Status1);
                    parameters.Add("@Status2", inspectionRecordDetail.Status2);
                    parameters.Add("@Status3", inspectionRecordDetail.Status3);
                    parameters.Add("@Status4", inspectionRecordDetail.Status4);
                    parameters.Add("@Email", inspectionRecordDetail.Email);
                    parameters.Add("@PermitNo", inspectionRecordDetail.PermitNo);
                    parameters.Add("@InspectionDate", inspectionRecordDetail.InspectionDate);
                    parameters.Add("@AmPm", inspectionRecordDetail.AmPm);
                    parameters.Add("@InspectionTypeId", inspectionRecordDetail.InspectionTypeId);
                    parameters.Add("@Comments", inspectionRecordDetail.Comments);
                    parameters.Add("@Contact", inspectionRecordDetail.Contact);
                    parameters.Add("@Type", inspectionRecordDetail.Type);
                    parameters.Add("@InspectionOrder", inspectionRecordDetail.InspectionOrder);
                    parameters.Add("@CreatedBy", inspectionRecordDetail.CreatedBy);
                    parameters.Add("@CreatedDate", createdDateForInspection);
                    parameters.Add("@UpdatedBy", inspectionRecordDetail.UpdatedBy);
                    parameters.Add("@UpdatedDate", DateTime.Now);
                    parameters.Add("@FirstName", inspectionRecordDetail.FirstName);
                    parameters.Add("@LastName", inspectionRecordDetail.LastName);
                    parameters.Add("@ResidentUserId", inspectionRecordDetail.ResidentUserId);
                    parameters.Add("@Acknowledge", inspectionRecordDetail.Acknowledge);
                    parameters.Add("@IsCancelled", inspectionRecordDetail.IsCancelled);
                    parameters.Add("@IsRejected", inspectionRecordDetail.IsRejected);
                    parameters.Add("@InspectorName", inspectionRecordDetail.InspectorName);
                    parameters.Add("@InspectionStatus", inspectionRecordDetail.InspectionStatus);
                    parameters.Add("@Apartment", inspectionRecordDetail.Apartment);

                    return await sqlConnection.QueryAsync<string>("AddEditInspectionRecord", parameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionService] - AddInspectionDetail() Exception is : " + ex);
                return null;
            }
        }

        public string DeleteInspectionById(int id)
        {
            try
            {
                using (GlobalDBContext db = new GlobalDBContext())
                {
                    var inspe = db.Inspections.Where(c => c.Id == id).FirstOrDefault();
                    db.Inspections.Remove(inspe);
                    db.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in InspectionService] - DeleteInspectionById() Exception is : " + ex);
                return "Fail";
            }
        }
        

        public async Task<InspctionViewModel> GetInspectionDetailsById(int incidentId)
        {
            try
            {
                InspctionViewModel inspectionDetailsModel = new InspctionViewModel();
                using (var sqlConnection = new SqlConnection(connectionstring))
                {
                    await sqlConnection.OpenAsync();
                    var parameters = new DynamicParameters();
                    parameters.Add("@InspectionId", incidentId);
                    inspectionDetailsModel = await sqlConnection.QueryFirstAsync<InspctionViewModel>("GetInspectionById", parameters,
                        commandType: CommandType.StoredProcedure);
                }
                return inspectionDetailsModel;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in InspectionService] - DeleteInspectionById() Exception is : " + ex);
                return null;
            }
        }

        public async Task<Inspection> UpdateInspectionInspector(int id, string inspector)
        {
            try
            {
                var resultUpdate = await _GlobalDBContext.Inspections.Where(x => x.Id == Convert.ToInt32(id)).FirstOrDefaultAsync();

                
                resultUpdate.InspectorName = inspector;


                var result = _GlobalDBContext.Inspections.Update(resultUpdate);
                await _GlobalDBContext.SaveChangesAsync();

                return resultUpdate;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionService] - UpdateInspectionInspector() Exception is : " + ex);
                return null;
            }
        }

        public async Task<Inspection> UpdateInspectionTime(int id, string time)
        {
            try
            {
                var resultUpdate = await _GlobalDBContext.Inspections.Where(x => x.Id == Convert.ToInt32(id)).FirstOrDefaultAsync();


                resultUpdate.AmPm = time;


                var result = _GlobalDBContext.Inspections.Update(resultUpdate);
                await _GlobalDBContext.SaveChangesAsync();

                return resultUpdate;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionService] - UpdateInspectionInspector() Exception is : " + ex);
                return null;
            }
        }

        public async Task<Inspection> UpdateInspectionDate(int id, string date)
        {
            try
            {
                var resultUpdate = await _GlobalDBContext.Inspections.Where(x => x.Id == Convert.ToInt32(id)).FirstOrDefaultAsync();


                resultUpdate.InspectionDate = Convert.ToDateTime(date);


                var result = _GlobalDBContext.Inspections.Update(resultUpdate);
                await _GlobalDBContext.SaveChangesAsync();

                return resultUpdate;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [InspectionService] - UpdateInspectionInspector() Exception is : " + ex);
                return null;
            }
        }
    }
}
