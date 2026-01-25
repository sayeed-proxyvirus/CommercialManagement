using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Services;
using CommercialManagement.Services.ServiceImple;
using CommercialManagement.Services.Exports;
using CommercialManagement.Services.Exports.ExportsServiceImple;
using SAMLitigation.Services;
using SAMLitigation.Services.ServiceImple;
using Microsoft.EntityFrameworkCore;
using CommercialManagement.Services.Logins.LoginServices;
using CommercialManagement.Services.DropDownSerivces;
using CommercialManagement.Services.DropDownSerivces.DropDownSerivceImple;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<CommercialDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CommercialDBConn")));

// Add Session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".CommercialManagement.Session";
});

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Register Services
builder.Services.AddScoped<UserService, UserServiceImple>();
builder.Services.AddScoped<CustomerService, CustomerServiceImple>();
builder.Services.AddScoped<BeneficiaryService, BeneficiaryServiceImple>();
builder.Services.AddScoped<ApplicantConsigneesService, ApplicantConsigneesServiceImple>();
builder.Services.AddScoped<NotifyingPartyService, NotifyingPartyServiceImple>();
builder.Services.AddScoped<PartyService, PartyServiceImple>();
builder.Services.AddScoped<FabricsService, FabricsServiceImple>();
builder.Services.AddScoped<DynamicMenuService, DynamicMenuServiceImple>();
builder.Services.AddScoped<ExportDataService, ExportDataServiceImple>();
builder.Services.AddScoped<DropDownService, DropDownServiceImple>();
//builder.Services.AddScoped<ExportInvoiceService, ExportInvoiceServiceImple>();
builder.Services.AddScoped<ExportMainService, ExportMainServiceImple>();
builder.Services.AddScoped<ExportLCItemsService, ExportLCItemsServiceImple>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable Session
app.UseSession();

app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();