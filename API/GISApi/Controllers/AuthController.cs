using AutoMapper;
using GISApi.Auth;
using GISApi.Data;
using GISApi.Data.GlobalEntities;
using GISApi.Helpers;
using GISApi.Models;
using GISApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.InkML;


namespace GISApi.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ILogger<AuthController> _logger;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IHttpContextAccessor _accessor;
        private readonly IRolesService _rolesService;
        private readonly GlobalDBContext _GlobalDBContext;
        private IUserService _userService;
        private readonly IMapper _mapper;
        //private readonly IEmailService _emailService;

        public AuthController(
           IConfiguration config,

           UserManager<ApplicationUser> userManager,
           ILogger<AuthController> logger,
           IJwtFactory jwtFactory, IWebHostEnvironment hostingEnvironment,
           IOptions<JwtIssuerOptions> jwtOptions,
           IHttpContextAccessor accessor,
           IRolesService rolesService,
           GlobalDBContext GlobalDBContext,
           IMapper mapper,
            IUserService userService,
           RoleManager<IdentityRole> roleManager
           //IEmailService emailService

           )
        {
            _logger = logger;
          //  _emailService = emailService;
            _mapper = mapper;
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _webHostEnvironment = hostingEnvironment;
            _GlobalDBContext = GlobalDBContext;
            _config = config;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            _accessor = accessor;
            _rolesService = rolesService;
        }

        /// <summary>
        /// login
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [EnableCors("AllowSpecificOrigin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CredentialsViewModel credentials)
        {
         
            if (!ModelState.IsValid)
            {
                return BadRequest(new CustomBadRequest(ModelState));
            }
            try
            {

                //get logged-in user
                var user = await _userManager.FindByNameAsync(credentials.UserName);

                if (user == null)
                {
                    ModelState.AddModelError("ERROR", "Invalid credentials, Please try again.");
                    return BadRequest(new { errorText = "Invalid" });
                }
                if (!user.IsActive)
                {
                    ModelState.AddModelError("ERROR", "Your account is inactive, Please Contact Admin.");
                    return BadRequest(new { errorText = "Deactivated" });
                }

               var passwordok = await _userManager.CheckPasswordAsync(user, credentials.Password);
                //// check the credentials              
                var dateTime = DateTime.Now;

                // check if user is lock


                // user failed attempt check
                AspNetUser userResult = _GlobalDBContext.AspNetUsers.Where(x => x.Id == user.Id).FirstOrDefault();
                //var newUser = _mapper.Map<AddEditUserViewModel>(userResult);
                AddEditUserViewModel newUser = await _userService.GetUserById(user.Id);
                //int timeSpam = userResult.FailedAttemptsDateTime - DateTime.Now;

                               
                if (passwordok == null || !passwordok)
                {
                    ModelState.AddModelError("login_failure", "Invalid credentials, Please try again.");
                   
                    // increse faild attempts
                    int passWordFailedAttempts = 0;
                    passWordFailedAttempts = passWordFailedAttempts + 1;                  
                    var userEdit = await _userService.EditUserForAttempts(newUser);



                    return BadRequest(new { errorText = "Invalid" });
                }



                // get claims identity
                var roles = await _userManager.GetRolesAsync(user);
                var identity = _jwtFactory.GenerateClaimsIdentity(credentials.UserName, user.Id, roles.ToList());
              
                dynamic response = new
                {
                    authToken = await _jwtFactory.GenerateEncodedToken(credentials.UserName, identity, user.RoleId),
                    expiresIn = (int)_jwtOptions.ValidFor.TotalSeconds,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    role = roles[0],
                    userId = user.Id,
                };               

                if (user != null)
                {
                    IdentityResult result = await _userManager.UpdateSecurityStampAsync(user);
                }
                var json = JsonConvert.SerializeObject(response, _serializerSettings);
                return new OkObjectResult(json);
            }
            catch (Exception ex)
            {
                _logger.LogError("[" + nameof(AuthController) + "." + nameof(Post) + "]" + ex);
                ModelState.AddModelError("ERROR", "[" + nameof(AuthController) + "]");
                return BadRequest(new CustomBadRequest(ModelState));
            }
        }



        [HttpPost]
        [AllowAnonymous]
        [Route("updatepassword")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> updatepassword([FromBody] ResetPasswordModel model)
        {
            try
            {
                _logger.LogError("[api/account/ResetPassword] Info: Started" + model.UserId);
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    _logger.LogError("[api/account/ResetPassword] Info: Bad request");
                    return BadRequest();
                }
                ApplicationUser userapp = await _userManager.FindByIdAsync(model.UserId);

                UserResetPassword userResetPassword;
                UserResetPassword usrp = new UserResetPassword();
                var userResetPasswordurl = await _GlobalDBContext.UserResetPasswords.FindAsync(Convert.ToInt32(model.Code));
                if (userResetPasswordurl == null)
                {
                    _logger.LogError("[api/account/ResetPassword] error: Try after 24 hours");
                    return Ok(new { errorText = "BadError" });
                }
                else
                {
                    userResetPassword = await _userService.GetResetPassUrl(Convert.ToInt32(model.Code));
                }

                string codes = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(userResetPassword.Code));
                //string codes = "";
                if (userapp.EmailConfirmed)
                {
                    var result = await _userManager.ResetPasswordAsync(userapp, codes, model.ConfirmPassword);
                    if (!result.Succeeded)
                    {
                        _logger.LogError("[api/account/ResetPassword] error: Password Not Changed");
                        return Ok(new { errorText = "Error" });
                    }
                    else
                    {
                        _logger.LogInformation("Password Changed for user" + userapp.Email);
                        return Ok(new { errorText = "Success" });

                    }

                }
                else
                {
                    _logger.LogError("[api/account/ResetPassword] error: Email Not Confirmed");
                    return Ok(new { errorText = "Error" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("[api/account/ResetPassword] error: " + ex);
                return Ok(new { errorText = "Error" });
            }
            //return Ok();
        }


        /// <summary>
        /// ResendEmail
        /// </summary>
        /// <returns></returns>
        [HttpPost("ResendEmamil")]
        public async Task<IActionResult> ResendEmail([FromBody] UserNameViewModel credentials)
        {
            int Count = 0;
            int minutesDifference = 0;

            try
            {

                string userId = User.Claims.Single(c => c.Type == ClaimTypes.PrimarySid).Value;
                AspNetUser userResult = _GlobalDBContext.AspNetUsers.FirstOrDefault(x => x.Id == userId);
                AddEditUserViewModel newUser = await _userService.GetUserById(userResult.Id);


             
                // reset Count

               
                var userEditCount = await _userService.GenerateOtpIncreseCount(newUser);

                Random random = new Random();
                // Generate a random 4-digit number
                int randomNumber = random.Next(100000, 1000000);

               
                var userEdit = await _userService.GenerateOtp(newUser);


                return Ok();



            }
            catch (Exception ex)
            {
                _logger.LogError("[" + nameof(AuthController) + "." + nameof(ResendEmail) + "]" + ex);
                ModelState.AddModelError("ERROR", "[" + nameof(AuthController) + "]");
                return BadRequest(new CustomBadRequest(ModelState));
            }
        }


        /// <summary>
        /// OtpVerify
        /// </summary>
        /// <returns></returns>
        [HttpPost("OtpCheck")]
        public async Task<IActionResult> OtpCheck([FromBody] OtpViewModel credentials)
        {
            int Count = 0;
            int minutesDifference = 0;

            try
            {

                string userId = User.Claims.Single(c => c.Type == ClaimTypes.PrimarySid).Value;
                AspNetUser userResult = _GlobalDBContext.AspNetUsers.FirstOrDefault(x => x.Id == userId);
                AddEditUserViewModel newUser = await _userService.GetUserById(userResult.Id);
                int Otp = credentials.Otp;
                return Ok(new { success = true, message = "OTP is valid." });

            }
            catch (Exception ex)
            {
                _logger.LogError("[" + nameof(AuthController) + "." + nameof(OtpCheck) + "]" + ex);
                ModelState.AddModelError("ERROR", "[" + nameof(AuthController) + "]");
                return BadRequest(new CustomBadRequest(ModelState));
            }
        }



        /// <summary>
        /// logout
        /// </summary>
        /// <returns></returns>
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var loginRefId = User.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.LoginRefId);
                //  var user = await _userManager.FindByNameAsync(userId);
                string userId = User.Claims.Single(c => c.Type == ClaimTypes.PrimarySid).Value;

                DateTime dateTime = DateTime.Now;
                //LogViewModel logViewModel = new LogViewModel()
                //{
                //    LogId = Convert.ToInt32(userId),
                //    LogOutDate = dateTime.ToShortDateString(),
                //    LogOutTime = dateTime.ToLongTimeString()
                //};
                // await _logService.AddLog(logViewModel);
                if (userId != null)
                {

                    Userloginlog userList = new Userloginlog();

                    userList.UserId = userId;
                    userList.Logoutdatetime = DateTime.Now;
                    userList.Type = "Logout";


                    //await _GlobalDBContext.Userloginlogs.AddAsync(userList);
                    //await _GlobalDBContext.SaveChangesAsync();
                }


                var userListModel = _GlobalDBContext.AspNetUsers.FirstOrDefault(x => x.Id == userId);

                if (userListModel != null)
                {
                    // Update the authentication token
                  //  userListModel.AuthToken = "";
                    // Mark the entity as modified
                  //  _GlobalDBContext.Aspnetusers.Update(userListModel);
                   // _GlobalDBContext.SaveChangesAsync();
                }
                // refresh token at log out
                //var token = await HttpContext.GetTokenAsync("access_token");
                //if (!string.IsNullOrEmpty(token))
                //{
                //    // Store the token in a list of invalidated tokens
                //    InvalidateAccessToken(token);

                //    // Optionally, you might want to revoke the refresh token as well
                //    var refreshToken = await HttpContext.GetTokenAsync("refresh_token");
                //    if (!string.IsNullOrEmpty(refreshToken))
                //    {
                //        InvalidateRefreshToken(refreshToken);
                //    }
                //    Response.Cookies.Delete("access_token");
                //}


                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("[" + nameof(AuthController) + "." + nameof(Logout) + "]" + ex);
                ModelState.AddModelError("ERROR", "[" + nameof(AuthController) + "]");
                return BadRequest(new CustomBadRequest(ModelState));
            }
        }

        // POST: api/auth/ForgotPassword
        /// <summary>
        /// forgot password
        /// </summary>
        /// <param name="model">ForgotPasswordModel</param>
        /// <returns>Task<IHttpActionResult></returns>
        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new CustomBadRequest(ModelState));
            }
            try
            {
                ApplicationUser applicationUser = await _userManager.FindByNameAsync(model.Email);
                if (applicationUser == null)
                {
                    ModelState.AddModelError("ERROR", "The email address provided is not registered.");
                    return BadRequest(new CustomBadRequest(ModelState));
                }

                if (!applicationUser.IsActive)
                {
                    ModelState.AddModelError("ERROR", "Your account is disabled. Please contact JPS support team for assistance.");
                    return BadRequest(new CustomBadRequest(ModelState));
                }

                //Send an email with this link
                string code = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
                code = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(code));
                var urlId = await _userService.AddResetPassUrl(code, applicationUser.Id);
                //Generate password reset callbacak URL
                //var callBackUrl = GenerateResetPwdCallbackUrl(code, applicationUser.Id);

                //Email
                //string subject = "Reset JPS ERP Account Password";
                //string htmlBody = string.Empty;
                //string contentRootPath = _webHostEnvironment.ContentRootPath;
                //using (StreamReader sr = new StreamReader(contentRootPath + "/EmailTemplate/ResetPasswordEmail.html"))
                //{
                //    htmlBody = sr.ReadToEnd();
                //}
                //htmlBody = htmlBody.Replace("##hrefCode##", callBackUrl);
                //htmlBody = htmlBody.Replace("##user_name##", applicationUser.FirstName + " " + applicationUser.LastName);
                //return Ok();
                //var success = _emailService.SendEmail(applicationUser.Email, subject, htmlBody);

                if (true) //success
                    return Ok(new { id = applicationUser.Id + "&&" + urlId });
                else
                {
                    ModelState.AddModelError("ERROR", "Could not send email to your registered email account. Please try again.");
                    return BadRequest(new CustomBadRequest(ModelState));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("[" + nameof(AuthController) + "." + nameof(ForgotPassword) + "]" + ex);
                ModelState.AddModelError("ERROR", "[" + nameof(AuthController) + "]");
                return BadRequest(new CustomBadRequest(ModelState));
            }
        }

        // POST: api/Auth/ResetPassword
        /// <summary>
        /// reset password for action type accountactivation/ forgotpassword
        /// </summary>
        /// <param name="model">ResetPasswordModel</param>
        /// <returns>Task<IHttpActionResult></returns>
        //[HttpPost("resetpassword")]
        //[AllowAnonymous]
        //public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(new CustomBadRequest(ModelState));
        //        }
        //        string[] actionTypes = { "forgotpassword", "accountactivation", "reset" };
        //        //check actiontype provided in request
        //        if (!actionTypes.Contains(model.ActionType))
        //        {
        //            ModelState.AddModelError("ERROR", "Please provide correct URL");
        //            return BadRequest(new CustomBadRequest(ModelState));
        //        }
        //        ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError("ERROR", "The email address provided is not registered.");
        //            return BadRequest(new CustomBadRequest(ModelState));
        //        }
        //        if (!user.IsActive)
        //        {
        //            ModelState.AddModelError("ERROR", "Your account is disabled. Please contact JPS Support team for further assistance.");
        //            return BadRequest(new CustomBadRequest(ModelState));
        //        }

        //        string code = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(model.Code));

        //        var result = await _userManager.ResetPasswordAsync(user, code, model.ConfirmPassword);
        //        if (!result.Succeeded)
        //        {
        //            return GetErrorResult(result);
        //        }
        //        return Ok();


        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("[" + nameof(AuthController) + "." + nameof(ResetPassword) + "]" + ex);
        //        ModelState.AddModelError("ERROR", "[" + nameof(AuthController) + "]");
        //        return BadRequest(new CustomBadRequest(ModelState));
        //    }
        //}

        /// <summary>
        /// Change passowrd from UI- API Calls
        /// </summary>
        /// <param name="model">ChangePasswordModel</param>
        /// <returns></returns>
        [HttpPost("changepassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new CustomBadRequest(ModelState));
            }
            try
            {
                string userId = User.Claims.Single(c => c.Type == ClaimTypes.PrimarySid).Value;
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    ModelState.AddModelError("ERROR", "Invalid user Or password");
                    return BadRequest(new CustomBadRequest(ModelState));
                }

                //New password should not be same as old password.
                bool checkOriginalPassword = await _userManager.CheckPasswordAsync(user, model.NewPassword);
                if (checkOriginalPassword)
                {
                    ModelState.AddModelError("ERROR", "New password must be different than your previous password.");
                    return BadRequest(new CustomBadRequest(ModelState));
                }

                IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (!result.Succeeded)
                {

                    return GetErrorResult(result);
                }

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError("[" + nameof(AuthController) + "." + nameof(ChangePassword) + "]" + ex);
                ModelState.AddModelError("ERROR", "[" + nameof(AuthController) + "]");
                return BadRequest(new CustomBadRequest(ModelState));
            }
        }

        #region private methods


        /// <summary>
        /// Add login entry to logs table
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        //private async Task<LogViewModel> AddLogEntry(string userName)
        //{
        //    DateTime dateTime = DateTime.Now;
        //    LogViewModel logViewModel = new LogViewModel()
        //    {
        //        LogId = 0,
        //        LogInDate = dateTime.Date,
        //        LogInTime = dateTime,
        //        LogInStamp = dateTime.ToString(),
        //        OtherInfo = GetUserBrowserDetails(),
        //        Ipaddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
        //        Username = userName,
        //    };
        //   // var resultLogViewModel = await _logService.AddLog(logViewModel);
        //    //return resultLogViewModel;
        //}

        private string GenerateResetPwdCallbackUrl(string code, string actionType)
        {
            string uciGuiLink = _config["ERPGUILinkReset"];

            return Microsoft.AspNetCore.Http.Extensions.UriHelper.Encode(
                new System.Uri($"{uciGuiLink}{actionType}&&{HttpUtility.UrlEncode(code)}"));
        }


        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(new CustomBadRequest(ModelState));
                }
            }
            return BadRequest();
        }

        private string GetUserBrowserDetails()
        {
            var userAgent = !String.IsNullOrEmpty(_accessor.HttpContext.Request.Headers["User-Agent"])
                                ? _accessor.HttpContext.Request.Headers["User-Agent"].ToString()
                                : string.Empty;
            return userAgent;
        }

        private List<RouteViewModel> GetRoutes(List<string> roles, List<string> userPermissions)
        {
            List<RouteViewModel> defaultRoutes = ReadDefaultRoute();
            if (roles.Contains("BackendAdmin") || roles.Contains("SuperAdmin"))
            {
                return defaultRoutes;
            }
            List<RouteViewModel> userRoutes = new List<RouteViewModel>();
            #region menu
            foreach (var userPermission in userPermissions)
            {
                string[] permissionSplitArray = userPermission.Split(".");
                var menuRoute = defaultRoutes.Where(x => x.title == permissionSplitArray[1]).FirstOrDefault();
                if (menuRoute != null)
                {
                    RouteViewModel route = GetRoute(menuRoute);
                    if (!userRoutes.Any(x => x.title.ToLower() == route.title.ToLower()))
                    {
                        userRoutes.Add(route);
                    }
                }
            }
            #endregion
            #region submenu
            foreach (var userRoute in userRoutes)
            {
                foreach (var userPermission in userPermissions)
                {
                    string[] permissionSplitArray = userPermission.Split(".");
                    if (permissionSplitArray.Contains("Account/Finance Settings"))
                    {
                        string str = "";
                    }
                    if (userRoute.title.ToLower() == permissionSplitArray[1].ToLower())
                    {
                        var defaultRoute = defaultRoutes
                            .Where(x => x.title.ToLower() == permissionSplitArray[1].ToLower()).FirstOrDefault();
                        if (defaultRoute != null)
                        {
                            var defaultSubmenus = defaultRoute.submenu
                                .Where(x => x.title.ToLower() == permissionSplitArray[2].ToLower()).ToList();
                            foreach (var submenuRoute in defaultSubmenus)
                            {
                                var route = GetRoute(submenuRoute);
                                if (!userRoute.submenu.Any(x => x.title.ToLower() == route.title.ToLower()))
                                {
                                    userRoute.submenu.Add(route);
                                    userRoute.submenu.OrderBy(x => x.index).ToList();
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            #region sub-submenu
            foreach (var userRoute in userRoutes)
            {
                foreach (var userPermission in userPermissions)
                {
                    string[] permissionSplitArray = userPermission.Split(".");
                    if (userRoute.title == permissionSplitArray[1])
                    {
                        foreach (var userSubmenu in userRoute.submenu)
                        {
                            var defaultRoute = defaultRoutes
                                .Where(x => x.title == permissionSplitArray[1]).FirstOrDefault();
                            if (defaultRoute != null)
                            {
                                string[] subPermissionArray = permissionSplitArray[2].Split("-");
                                var defaultSubmenus = defaultRoute.submenu
                                    .Where(x => x.title == subPermissionArray[0]).ToList();
                                if (userSubmenu.title == subPermissionArray[0])
                                {
                                    foreach (var submenuRoute in defaultSubmenus)
                                    {
                                        if (subPermissionArray.Count() > 1)
                                        {
                                            var defaultSubSubMenu = submenuRoute.submenu
                                            .Where(x => x.title == subPermissionArray[1]).FirstOrDefault();
                                            if (defaultSubSubMenu != null)
                                            {
                                                var route = GetRoute(defaultSubSubMenu);
                                                if (!userSubmenu.submenu.Any(x => x.title == route.title))
                                                {
                                                    userSubmenu.submenu.Add(route);
                                                    userRoute.submenu.OrderBy(x => x.index).ToList();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            foreach (var item in userRoutes)
            {
                item.submenu = item.submenu.OrderBy(x => x.index).ToList();
            }
            return userRoutes.OrderBy(x => x.index).ToList();

        }

        private List<RouteViewModel> ReadDefaultRoute()
        {
            List<RouteViewModel> routeViewModels = new List<RouteViewModel>();
            using (StreamReader r = new StreamReader($"{_webHostEnvironment.ContentRootPath}\\MenuInfo.json"))
            {
                string json = r.ReadToEnd();
                routeViewModels = JsonConvert.DeserializeObject<List<RouteViewModel>>(json);
            }
            return routeViewModels.OrderBy(x => x.index).ToList();
        }

        private RouteViewModel GetRoute(RouteViewModel menuRoute)
        {
            RouteViewModel route = new RouteViewModel()
            {
                className = menuRoute.className != null ? menuRoute.className : "",
                extralink = menuRoute.extralink,
                icon = menuRoute.icon,
                path = menuRoute.path,
                submenu = new List<RouteViewModel>(),
                title = menuRoute.title,
                index = menuRoute.index
            };
            return route;
        }

        public static string Decrypt(string cipherText)
        {
            try
            {
                var keyBytes = Encoding.UTF8.GetBytes("9876543210456789");
                var iv = Encoding.UTF8.GetBytes("4567890123456789");

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = keyBytes;
                    aesAlg.IV = iv;
                    aesAlg.Mode = CipherMode.CBC;  // Use the same mode as in CryptoJS
                    aesAlg.Padding = PaddingMode.PKCS7;  // Use the same padding as in CryptoJS

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error in Decrypt: {ex.Message}");
                return null;
            }
        }

        public static string DecryptBytesToString_Aes(byte[] cipherText)
        {
            try
            {
                var keyBytes = Encoding.UTF8.GetBytes("987654321045678945659845");
                var iv = Encoding.UTF8.GetBytes("4567890123456789");

                using (AesManaged aesAlg = new AesManaged())
                {
                    aesAlg.Key = keyBytes;
                    aesAlg.IV = iv;
                    aesAlg.Mode = CipherMode.CFB;  // Adjust the mode based on your encryption settings
                    aesAlg.Padding = PaddingMode.None;  // No padding

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        var decryptedText = srDecrypt.ReadToEnd();
                        return decryptedText;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error in DecryptBytesToString_Aes: {ex.Message}");
                return null;
            }
        }





        #endregion
    }
}
