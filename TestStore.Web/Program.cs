using System.IdentityModel.Tokens.Jwt;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Usecases;
using TestStore.Web.Core;
using TestStore.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);
builder.Services.AddSingleton(appSettings);
builder.Services.AddJwt(appSettings);
builder.Services.AddActionUser();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<AuthService>();

builder.Services.AddTransient<TestStoreDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddTransient<UsecaseHandler>();
builder.Services.AddTransient<JwtSecurityTokenHandler>();
builder.Services.AddUsecases();
builder.Services.AddValidators();
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
app.UseMiddleware<GlobalExceptionHandler>();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
