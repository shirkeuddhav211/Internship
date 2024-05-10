using AutoMapper;
using Dapper;
using DocumentFormat.OpenXml.InkML;
using GISApi.Data;
using GISApi.Data.GlobalEntities;
using GISApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Services
{
    public interface IUserService
    {
        Task<List<AddEditUserViewModel>> GetUsers(bool? status, int organizationId, string roleName);

        Task<AddEditUserViewModel> GetUserById(string userId);

        Task<UserProfileViewModel> GetUserProfileById(string userId);
        Task<AddEditUserViewModel> EditUser(AddEditUserViewModel model);
        //Task<AddEditUserViewModel> GetById(string id);

        Task<string> GetUserNameById(string id);
        Task<AddEditUserViewModel> EditUserForAttempts(AddEditUserViewModel model);
        Task<AddEditUserViewModel> GenerateOtp(AddEditUserViewModel model);
        Task<AddEditUserViewModel> GenerateOtpIncreseCount(AddEditUserViewModel model);
        Task<AddEditUserViewModel> ResetOtpFields(AddEditUserViewModel model);
        Task<IEnumerable<AddEditUserViewModel>> GetUserListAsync();
        Task<int> AddResetPassUrl(string code, string Id);
        Task<UserResetPassword> GetResetPassUrl(int Id);
    }

    public class UsersService : IUserService
    {
        private readonly IMapper _mapper;
        private GlobalDBContext _GlobalDBContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UsersService> _logger;
        private string connectionstring;
        private IConfiguration _iconnectionstring;
        public UsersService(IConfiguration iconnectionstring,ILogger<UsersService> logger, GlobalDBContext GlobalDBContext, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _GlobalDBContext = GlobalDBContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _iconnectionstring = iconnectionstring;
            connectionstring = _iconnectionstring["ConnectionStrings:DefaultConnection"];
        }






        /// <summary>
        /// Get users
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<List<AddEditUserViewModel>> GetUsers(bool? status, int organizationId, string roleName)
        {
            try
            {
                if (roleName == "SuperAdmin")
                {
                    var userList = (from x in _GlobalDBContext.AspNetUsers
                                    select new AddEditUserViewModel
                                    {
                                        Id = x.Id,
                                        UserName = x.UserName,
                                        Email = x.Email,
                                        //LastName = x.LastName,
                                        //FirstName = x.FirstName,
                                        //RoleId = x.RoleId,
                                        //RoleName = x.RoleName,
                                        //IsActive = (bool)x.IsActive,
                                        //PhoneNumber = x.PhoneNumber,
                                        //Address = x.Address,                                        
                                        //DisplayUserName = x.DisplayUserName,
                                    }).AsQueryable();

                    return userList.Where(x => x.RoleName != "SuperAdmin").ToList();
                }


                var userLists = (from x in _GlobalDBContext.AspNetUsers
                                 select new AddEditUserViewModel
                                 {
                                     Id = x.Id,
                                     UserName = x.UserName,
                                     Email = x.Email,
                                     //LastName = x.LastName,
                                     //FirstName = x.FirstName,
                                     //RoleId = x.RoleId,
                                     //RoleName = x.RoleName,
                                     //IsActive = (bool)x.IsActive,
                                     //PhoneNumber = x.PhoneNumber,
                                     //Address = x.Address,
                                     //DisplayUserName = x.DisplayUserName
                                 });


                return userLists.Where(x => x.RoleName != "SuperAdmin").ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError("[" + nameof(UsersService) + "." + nameof(GetUsers) + "]" + ex);
                throw ex;
            }
        }


        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<AddEditUserViewModel> GetUserById(string userId)
        {
            try
            {
                var userInfo = await (from user in _GlobalDBContext.AspNetUsers
                                          //join userrole in _GlobalDBContext.Aspnetuserroles on user.Id equals userrole.UserId
                                          //join role in _GlobalDBContext.Aspnetroles on user.RoleId equals role.Id
                                      where user.Id == userId
                                      select new AddEditUserViewModel
                                      {
                                          Id = user.Id,
                                          UserName = user.UserName,
                                          Email = user.Email,   
                                          FirstName = user.FirstName,
                                          LastName = user.LastName,
                                          RoleId = user.RoleId,
                                          RoleName = user.RoleName,
                                          IsActive = (bool)user.IsActive,
                                          PhoneNumber = user.PhoneNumber,
                                          DisplayUserName = user.DisplayUserName,
                                          newPassword = "xxzzxx",
                                          Department = user.Department,
                                          Address = user.Address,
                                          AddressLine1 = user.AddressLine1,
                                          AddressLine2 = user.AddressLine2,
                                          City = user.City,
                                          Alias = user.Alias,
                                          UpdatedBy = user.UpdatedBy,
                                          UpdatedDate = user.UpdatedDate,
                                          State = user.State,
                                          Apartment = user.Apartment


                                      }).FirstOrDefaultAsync();
                return userInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError("[" + nameof(UsersService) + "." + nameof(GetUserById) + "]" + ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get user profile by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UserProfileViewModel</returns>
        public async Task<UserProfileViewModel> GetUserProfileById(string userId)
        {
            try
            {

                var userProfile = await (from u in _GlobalDBContext.AspNetUsers
                                         where u.Id == userId
                                         select new UserProfileViewModel
                                         {
                                             UserId = u.Id,
                                             //FirstName = u.FirstName,
                                             //LastName = u.LastName,
                                             PhoneNumber = u.PhoneNumber,
                                             //Position = u.Position,

                                         }).FirstOrDefaultAsync();


                return userProfile;
            }
            catch (Exception ex)
            {
                _logger.LogError("[" + nameof(UsersService) + "." + nameof(GetUserProfileById) + "]" + ex);
                throw ex;
            }
        }

        public async Task<AddEditUserViewModel> EditUser(AddEditUserViewModel model)
        {
            try
            {

                var userList = _GlobalDBContext.AspNetUsers.Where(x => x.Id == model.Id).FirstOrDefault();
                var newUser = _mapper.Map<ApplicationUser>(model);
                string RoleId = _roleManager.Roles.Where(x => x.Name == model.RoleName).Select(x => x.Id).FirstOrDefault();
                var role = _roleManager.Roles.Where(x => x.Id == RoleId).FirstOrDefault();
                var passwd = await _userManager.CreateAsync(newUser, model.Password);
                userList.UserName = model.UserName;
                userList.PhoneNumber = model.PhoneNumber;
                //userList.PasswordHash = Convert.ToString(passwd);

                var result = _GlobalDBContext.AspNetUsers.Update(userList);
                _GlobalDBContext.SaveChanges();


                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [BadgeAuditDataProvider] - AddEditBadgeAuditDetails() Exception is : " + ex);
                return null;
            }

        }

        public async Task<AddEditUserViewModel> EditUserForAttempts(AddEditUserViewModel model)
        {
            try
            {

                var userList = _GlobalDBContext.AspNetUsers.Where(x => x.Id == model.Id).FirstOrDefault();
                var newUser = _mapper.Map<ApplicationUser>(model);
                var result = _GlobalDBContext.AspNetUsers.Update(userList);
                _GlobalDBContext.SaveChanges();


                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [BadgeAuditDataProvider] - AddEditBadgeAuditDetails() Exception is : " + ex);
                return null;
            }



        }


        // generate Otp
        public async Task<AddEditUserViewModel> GenerateOtp(AddEditUserViewModel model)
        {
            try
            {

                var userList = _GlobalDBContext.AspNetUsers.Where(x => x.Id == model.Id).FirstOrDefault();
                var newUser = _mapper.Map<ApplicationUser>(model);
                var result = _GlobalDBContext.AspNetUsers.Update(userList);
                _GlobalDBContext.SaveChanges();


                return model;
            }



            catch (Exception ex)
            {
                _logger.LogError("Error occured in [BadgeAuditDataProvider] - AddEditBadgeAuditDetails() Exception is : " + ex);
                return null;
            }
        }

        public async Task<AddEditUserViewModel> GenerateOtpIncreseCount(AddEditUserViewModel model)
        {
            try
            {

                var userList = _GlobalDBContext.AspNetUsers.Where(x => x.Id == model.Id).FirstOrDefault();
                var newUser = _mapper.Map<ApplicationUser>(model);
                //string RoleId = _roleManager.Roles.Where(x => x.Name == model.RoleName).Select(x => x.Id).FirstOrDefault();
                //var role = _roleManager.Roles.Where(x => x.Id == RoleId).FirstOrDefault();
                //var passwd = await _userManager.CreateAsync(newUser, model.Password);
                //userList.UserName = model.UserName;
                //userList.PhoneNumber = model.PhoneNumber;
                ////userList.FirstName = model.FirstName;
                ////userList.LastName = model.LastName;
                ////userList.Address = model.Address;
                ////userList.RoleId = RoleId;
                //userList.PasswordHash = Convert.ToString(passwd);
                var result = _GlobalDBContext.AspNetUsers.Update(userList);
                _GlobalDBContext.SaveChanges();


                return model;
            }



            catch (Exception ex)
            {
                _logger.LogError("Error occured in [BadgeAuditDataProvider] - AddEditBadgeAuditDetails() Exception is : " + ex);
                return null;
            }
        }

        public async Task<AddEditUserViewModel> ResetOtpFields(AddEditUserViewModel model)
        {
            try
            {

                var userList = _GlobalDBContext.AspNetUsers.Where(x => x.Id == model.Id).FirstOrDefault();
                var newUser = _mapper.Map<ApplicationUser>(model);
                //string RoleId = _roleManager.Roles.Where(x => x.Name == model.RoleName).Select(x => x.Id).FirstOrDefault();
                //var role = _roleManager.Roles.Where(x => x.Id == RoleId).FirstOrDefault();
                //var passwd = await _userManager.CreateAsync(newUser, model.Password);
                //userList.UserName = model.UserName;
                //userList.PhoneNumber = model.PhoneNumber;
                ////userList.FirstName = model.FirstName;
                ////userList.LastName = model.LastName;
                ////userList.Address = model.Address;
                ////userList.RoleId = RoleId;
                //userList.PasswordHash = Convert.ToString(passwd);


                var result = _GlobalDBContext.AspNetUsers.Update(userList);
                _GlobalDBContext.SaveChanges();


                return model;
            }



            catch (Exception ex)
            {
                _logger.LogError("Error occured in [BadgeAuditDataProvider] - AddEditBadgeAuditDetails() Exception is : " + ex);
                return null;
            }



        }
        public async Task<string> GetUserNameById(string id)
        {
            string username = "";
            try
            {

                var userList = _GlobalDBContext.AspNetUsers.Find(id);
                if (userList != null)
                {
                    //username = userList.FirstName + " " + userList.LastName;
                }
                return username;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [BadgeAuditDataProvider] - GetUserNameById() Exception is : " + ex);
                return username;
            }
        }



        //public async Task<AddEditUserViewModel> GetById(string userId)
        //{
        //    try
        //    {
        //        using (GlobalDBContext db = new GlobalDBContext())
        //        {
        //            var testList = await _GlobalDBContext.Aspnetusers.Where(c => c.Id == userId).Select(x => new AddEditUserViewModel

        //            {
        //                UserName = x.UserName,
        //                PhoneNumber = x.PhoneNumber,
        //                FirstName = x.FirstName,
        //                LastName = x.LastName,
        //                Address = x.Address,
        //                Password = x.PasswordHash,
        //                IsActive = x.IsActive == 1 ? true : false,
        //                RoleId = x.Id


        //            }).FirstOrDefaultAsync();
        //            return testList;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error occured in [TestRecordsService] - GetList() Exception is : " + ex);
        //        return null;
        //    }
        //}

        public async Task<int> AddResetPassUrl(string code, string Id)
        {
            try
            {
                UserResetPassword userResetPassword = new UserResetPassword();

                // userResetPassword.Id = userResetPassword.Id;
                userResetPassword.UserId = Id;
                userResetPassword.Code = code;
                userResetPassword.Flag = 0;

                await _GlobalDBContext.UserResetPasswords.AddAsync(userResetPassword);
                await _GlobalDBContext.SaveChangesAsync();
                int rid = userResetPassword.Id;

                return rid;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [UserService] - AddResetPassUrl() Exception is : " + ex);

                return -1;
            }
        }

        public async Task<UserResetPassword> GetResetPassUrl(int Id)
        {
            UserResetPassword userResetPassword = new UserResetPassword();
            try
            {

                var userResetPasswordurl = await _GlobalDBContext.UserResetPasswords.FindAsync(Id)
        ;

                userResetPassword.Id = userResetPasswordurl.Id;
                userResetPassword.UserId = userResetPasswordurl.UserId;
                userResetPassword.Code = userResetPasswordurl.Code;
                userResetPassword.Flag = 1;

                _GlobalDBContext.UserResetPasswords.Remove(userResetPasswordurl);
                await _GlobalDBContext.SaveChangesAsync();
                return userResetPassword;



            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [UserService] - GetResetPassUrl() Exception is : " + ex);

                return userResetPassword;
            }
        }

        public async Task<IEnumerable<AddEditUserViewModel>> GetUserListAsync()
        {
            try
            {
                using (var sqlConnection = new SqlConnection(connectionstring))
                {
                    await sqlConnection.OpenAsync();
                    var result =  await sqlConnection.QueryAsync<AddEditUserViewModel>("GetAllUsers",
                        commandType: CommandType.StoredProcedure);
                    return result.Where(x => x.RoleName != "SuperAdmin");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in [UserService] - GetUserListAsync() Exception is : " + ex);
                return null;
            }
        }
    }
}
