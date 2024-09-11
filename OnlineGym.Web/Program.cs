using Microsoft.EntityFrameworkCore;
using OnlineGym.DataAccess.Data;
using OnlineGym.Entities.Repository;
using OnlineGym.DataAccess.RepositoryImplementation;
using Microsoft.AspNetCore.Identity;
using OnlineGym.Utilities;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using OnlineGym.Entities.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<OnlineGymContext>(options=>options.UseSqlServer(
    builder.Configuration.GetConnectionString("Conction")));

builder.Services.Configure<StripeDetails>(builder.Configuration.GetSection("stripe"));


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60))
    .AddDefaultTokenProviders().AddDefaultUI().AddEntityFrameworkStores<OnlineGymContext>();


builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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


StripeConfiguration.ApiKey = builder.Configuration.GetSection("stripe:Secretkey").Get<string>();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();


app.MapControllerRoute(
    name: "default",
    pattern: "{area=Client}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "client",
    pattern: "{area=Admin}/{controller=Employee}/{action=Index}/{id?}");
app.MapControllerRoute(
	name: "Coach",
	pattern: "{area=Coach}/{controller=Dashboard}/{action=Index}/{id?}");



app.Run();
