using FunWithFood.Helpers;
using FunWithFood.Interfaces;
using FunWithFood.Interfaces.Mappers;
using FunWithFood.Middleware;
using FunWithFood.Services;
using FunWithFoodDomain.Common;
using FunWithFoodDomain.Helpers;
using FunWithFoodDomain.Interfaces;
using FunWithFoodDomain.Interfaces.Common;
using FunWithFoodDomain.Interfaces.Mappers;
using FunWithFoodDomain.Services;
using FunWithFoodInfrastructure;
using FunWithFoodInfrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VideoGameOnlineShopDomain.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
// we will do this step AFTER we created our DbContext
// Context is the db context that we created.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FoodDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Food/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();
app.MapRazorPages();
app.UseMiddleware<DatabaseBootMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Food}/{action=Index}/{id?}");

app.Run();



void ConfigureServices(IServiceCollection services)
{
    #region Services Dependency Injections

    services.AddScoped<ICuisineApplicationService, CuisineApplicationService>();
    services.AddScoped<IMainCourseApplicationService, MainCourseApplicationService>();
    services.AddScoped<IAdminApplicationService, AdminApplicationService>();
    services.AddScoped<IHealthCheckApplicationService, HealthCheckApplicationService>();
    services.AddScoped<IDessertApplicationService, DessertApplicationService>();

    services.AddScoped<ICuisineService, CuisineService>();
    services.AddScoped<IMainCourseService, MainCourseService>();
    services.AddScoped<IAdminService, AdminService>();
    services.AddScoped<IHealthCheckService, HealthCheckService>();
    services.AddScoped<IDessertService, DessertService>();


    #endregion

    #region Repository Dependency Injections

    services.AddScoped<ICuisineRepository, CuisineRepository>();
    services.AddScoped<IMainCourseRepository, MainCourseRepository>();
    services.AddScoped<IAdminRepository, AdminRepository>();
    services.AddScoped<IHealthCheckRepository, HealthCheckRepository>();
    services.AddScoped<IDessertRepository, DessertRepository>();

    #endregion

    #region Common Dependency Injections

    services.AddScoped<IImageConversionService, ImageConversionService>();
    services.AddScoped<ICommonUtilityMethods, CommonUtilityMethods>();
    services.AddScoped<IAuthorizationUtilityMethods, AuthorizationUtilityMethods>();
    services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();

    services.AddScoped<ICuisineMapper, CuisineMapper>();
    services.AddScoped<ICuisineDomainMapper, CuisineDomainMapper>();

    services.AddScoped<IMainCourseMapper, MainCourseMapper>();
    services.AddScoped<IMainCourseDomainMapper, MainCourseDomainMapper>();

    services.AddScoped<IAdminMapper, AdminMapper>();
    services.AddScoped<IAdminDomainMapper, AdminDomainMapper>();

    services.AddScoped<IDessertMapper, DessertMapper>();
    services.AddScoped<IDessertDomainMapper, DessertDomainMapper>();

    #endregion

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Read token from the cookie if present
                if (context.Request.Cookies.TryGetValue("AuthToken", out var token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                context.HandleResponse(); 
                context.Response.Redirect("/Admin/LoginPage"); 
                return Task.CompletedTask;
            }
        };
    });
}
