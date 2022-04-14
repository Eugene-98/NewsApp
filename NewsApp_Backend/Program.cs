using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NewsApp.Data;
using NewsApp_Backend;
using Newtonsoft.Json.Serialization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(c =>
{
	c.AddPolicy("AllowOrigin", options => 
	options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddDbContext<Context>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("Online_storeContext")));

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
	.AddNewtonsoftJson(options =>
		options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
	.AddNewtonsoftJson(options=>options.SerializerSettings.ContractResolver=new DefaultContractResolver())
	.AddDataAnnotationsLocalization(options => {
		options.DataAnnotationLocalizerProvider = (type, factory) =>
			factory.Create(typeof(SharedResource));
	})
	.AddViewLocalization();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/login");
		options.AccessDeniedPath = new PathString("/Account/Login");
	});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	var supportedCultures = new[]
	{
					new CultureInfo("en"),
					new CultureInfo("ru")
				};

	options.DefaultRequestCulture = new RequestCulture("ru");
	options.SupportedCultures = supportedCultures;
	options.SupportedUICultures = supportedCultures;
});

builder.Services.AddControllers();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseRequestLocalization();

app.UseCors(options =>
	options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(
		Path.Combine(Directory.GetCurrentDirectory(), "Files")),
	RequestPath = "/Files"
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Admin}/{action=Index}/{id?}");

app.Run();
