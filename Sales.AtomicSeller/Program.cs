using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sales.AtomicSeller;
using Sales.AtomicSeller.Config;
using Sales.AtomicSeller.Consts;
using Sales.AtomicSeller.Data;
using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Enums;
using Sales.AtomicSeller.Extensions;
using Serilog;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
//builder.Configuration.AddJsonFile("serilog.json");
//builder.Configuration.AddJsonFile("identitydata.json");
//builder.Configuration.AddJsonFile("storedata.json");

builder.Configuration.AddEnvironmentVariables();

builder.Host.UseSerilog((hostContext, loggerConfig) =>
{
    loggerConfig
        .ReadFrom.Configuration(hostContext.Configuration)
        .Enrich.WithProperty("ApplicationName", hostContext.HostingEnvironment.ApplicationName);
}).ConfigureLogging((hostContext, logging) =>
{
    logging.AddSerilog();
});

IRootConfig rootConfiguration = new RootConfig();

builder.Configuration.GetSection(ConfigConsts.IdentityDataConfigKey).Bind(rootConfiguration.IdentityDataConfig);

builder.Configuration.GetSection(ConfigConsts.StoreDataConfigKey).Bind(rootConfiguration.StoreDataConfig);

builder.Configuration.GetSection(ConfigConsts.LangConfigKey).Bind(rootConfiguration.LangConfig);


builder.Configuration.GetSection(ConfigConsts.StripeConfigKey).Bind(rootConfiguration.StripeConfig);
builder.Configuration.GetSection(ConfigConsts.SMTPConfigKey).Bind(rootConfiguration.SMTPConfig);
builder.Configuration.GetSection(ConfigConsts.EmailCredConfigKey).Bind(rootConfiguration.EmailCredConfig);

builder.Configuration.GetSection(ConfigConsts.GoogleAuthConfigKey).Bind(rootConfiguration.GoogleAuthConfig);
builder.Configuration.GetSection(ConfigConsts.FacebookAuthConfigKey).Bind(rootConfiguration.FacebookAuthConfig);
builder.Configuration.GetSection(ConfigConsts.MicrosoftAuthConfigKey).Bind(rootConfiguration.MicrosoftAuthConfig);
builder.Configuration.GetSection(ConfigConsts.TwitterAuthConfigKey).Bind(rootConfiguration.TwitterAuthConfig);
builder.Configuration.GetSection(ConfigConsts.InvoiceConfigKey).Bind(rootConfiguration.InvoiceConfig);
builder.Services.AddSingleton(rootConfiguration);


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString(ConfigConsts.DbConnectionStringKey);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var identityService = builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Lockout.MaxFailedAccessAttempts = 20;
    options.User.RequireUniqueEmail = false;

})
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    //options.Cookie.Name = "StripeShopping";
    options.Cookie.Name = "SalesAtomicSeller";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
});
builder.Services.AddAuthentication()
   .AddGoogle(options =>
   {
       options.ClientId = rootConfiguration.GoogleAuthConfig.ClientId;
       options.ClientSecret = rootConfiguration.GoogleAuthConfig.ClientSecret;
   })
   .AddFacebook(options =>
   {
       options.ClientId = rootConfiguration.FacebookAuthConfig.ClientId;
       options.ClientSecret = rootConfiguration.FacebookAuthConfig.ClientSecret;
   })
   .AddMicrosoftAccount(microsoftOptions =>
   {
       microsoftOptions.ClientId = rootConfiguration.MicrosoftAuthConfig.ClientId;
       microsoftOptions.ClientSecret = rootConfiguration.MicrosoftAuthConfig.ClientSecret;
   })
   .AddTwitter(twitterOptions =>
   {
       twitterOptions.ConsumerKey = rootConfiguration.TwitterAuthConfig.ConsumerAPIKey;
       twitterOptions.ConsumerSecret = rootConfiguration.TwitterAuthConfig.ConsumerSecret;
       twitterOptions.RetrieveUserDetails = true;
   });

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
});

builder.Services.AddRazorPages();
builder.Services.InjectServices();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(IdentityConsts.ADMINISTRATOR_POLICY, policy => policy.RequireRole(RoleName.Administrator.ToString()));
    options.AddPolicy(IdentityConsts.USER_POLICY, policy => policy.RequireRole(RoleName.Administrator.ToString(), RoleName.User.ToString()));
});
StripeConfiguration.ApiKey = rootConfiguration.StripeConfig.SecretKey;
var app = builder.Build();

await Seeder.EnsureSeedData(app);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseMiddleware<Sales.AtomicSeller.Middlewares.ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.UseMiddleware<Sales.AtomicSeller.Middlewares.SerilogMiddleware>();

// Admin area
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
// Default
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
