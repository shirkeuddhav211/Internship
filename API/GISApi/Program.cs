using GISApi.Auth;
using GISApi.Data;
using GISApi.Models;
using GISApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

SymmetricSecurityKey _signingKey;
var builder = WebApplication.CreateBuilder(args);
// Read Appsetting.json for cors link
ConfigurationManager configuration = builder.Configuration;
var AllowSpecificOrigin = configuration.GetSection("AllowSpecificOrigin");
var Origins = AllowSpecificOrigin["Origins"]!;
var secretKey = configuration.GetSection("AuthSecretKey").ToString();
_signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

//Add Serilog
var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


//builder.Services.Configure<JsonOptions>(options =>
//{
//    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
//});

// Add this to the ConfigureServices routine in Startup.cs:
//JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
//{
//    PropertyNameCaseInsensitive = false,
//    //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
//    //DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
//    //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
//    //MaxDepth = 10,
//    //ReferenceHandler = ReferenceHandler.IgnoreCycles,
//    WriteIndented = true
//};
//serializerOptions.Converters.Add(new JsonStringEnumConverter());
//builder.Services.AddSingleton(s => serializerOptions);
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
}); 

//builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
//{
//    options.SerializerOptions.Converters.Add(new DateTimeConverterForCustomStandardFormatR());
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Add Swagger

var securityScheme = new OpenApiSecurityScheme()
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JSON Web Token based security",
};

var securityReq = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }
};

var contact = new OpenApiContact()
{
    Name = "Prashant Ashtekar",
    Email = "prashant_ashtekar@vaspsolutions.com",
    Url = new Uri("http://www.vasosolutions.com")
};

var license = new OpenApiLicense()
{
    Name = "Free License",
    Url = new Uri("http://www.astekbit.com")
};

var info = new OpenApiInfo()
{
    Version = "v1",
    Title = "GIS API",
    Description = "GIS API",
    TermsOfService = new Uri("http://www.vaspsolutions.com"),
    Contact = contact,
    License = license
};

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", info);
    o.AddSecurityDefinition("Bearer", securityScheme);
    o.AddSecurityRequirement(securityReq);
});



// jwt wire up
// Get options from app settings
var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

// Configure JwtIssuerOptions
builder.Services.Configure<JwtIssuerOptions>(options =>
{

    options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
    options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
    options.ValidFor = TimeSpan.FromMinutes(Convert.ToDouble(jwtAppSettingOptions[nameof(JwtIssuerOptions.ValidFor)]));
    options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(configureOptions =>
{
    configureOptions.RequireHttpsMetadata = false;
    configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
    configureOptions.SaveToken = true;
    configureOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = _signingKey,
        ValidateIssuer = true,
        ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],
        ValidateAudience = true,
        ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromDays(1)
    };
});

//Add Automapper
builder.Services.AddIdentityCore<ApplicationUser>(o =>
{
    // configure identity options
    o.Password.RequireDigit = true;
    o.Password.RequireLowercase = true;
    o.Password.RequireUppercase = true;
    o.Password.RequireNonAlphanumeric = true;
    o.Password.RequiredLength = 6;
}).AddErrorDescriber<CustomIdentityErrorDescriber>()
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
//builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);


builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.AddDbContext<GlobalDBContext>(ServiceLifetime.Transient);
builder.Services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Transient);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IRolesService, RolesService>();
builder.Services.AddTransient<IUserService, UsersService>();
builder.Services.AddTransient<IInspectionTypeService, InspectionTypeService>();
builder.Services.AddTransient<IInspectionService, InspectionService>();
builder.Services.AddTransient<IHolidayMasterService, HolidayMasterService>();
builder.Services.AddScoped<IJwtFactory, JwtFactory>();
builder.Services.AddScoped<ICommonService, CommonService>();


//Cors setting
builder.Services.AddCors(p => p.AddPolicy("AllowSpecificOrigin", builder =>
{
    string[] origins = configuration.GetSection("CorsAcceptableUrls:urls").Get<string[]>();
    builder.WithOrigins(Origins.Split(',').ToArray()).AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
      .RequireAuthenticatedUser()
      .Build();
});

builder.Services.AddMvc();
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

var app = builder.Build();
app.UseAuthentication();
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseMvcWithDefaultRoute();
// Add the middleware to hide headers
app.UseMiddleware<HideHeadersMiddleware>();
app.UseMvc();
app.UseAuthorization();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseRouting();

////app cors
//app.UseCors("AllowSpecificOrigin");

//app.UseHttpsRedirection();
//app.UseMvc();
//app.UseAuthorization();
//app.UseAuthentication();

//app.MapControllers();

//app.UseEndpoints(endpoints =>
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers().RequireAuthorization();
//});
app.Run();
