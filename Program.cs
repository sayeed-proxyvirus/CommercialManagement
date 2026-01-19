using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Services;
using CommercialManagement.Services.Exports;
using CommercialManagement.Services.Exports.ExportsServiceImple;
using CommercialManagement.Services.ServiceImple;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Register AppDbContext with connection string
var connectionString = builder.Configuration.GetConnectionString("CommercialDBConn");
builder.Services.AddDbContext<CommercialDBContext>(options =>
    options.UseSqlServer(connectionString));

// Register Services
builder.Services.AddScoped<ApplicantConsigneesService, ApplicantConsigneesServiceImple>();
builder.Services.AddScoped<BeneficiaryService, BeneficiaryServiceImple>();
builder.Services.AddScoped<CustomerService, CustomerServiceImple>();
builder.Services.AddScoped<FabricsService, FabricsServiceImple>();
builder.Services.AddScoped<NotifyingPartyService, NotifyingPartyServiceImple>();
builder.Services.AddScoped<PartyService, PartyServiceImple>();
builder.Services.AddScoped<ExportDataService, ExportDataServiceImple>();
builder.Services.AddScoped<ExportLCItemsService, ExportLCItemsServiceImple>();
builder.Services.AddScoped<ExportMainService, ExportMainServiceImple>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Login/Index";
        options.AccessDeniedPath = "/Login/AccessDenied";
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
 
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
