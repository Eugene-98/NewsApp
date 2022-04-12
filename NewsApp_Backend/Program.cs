using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NewsApp.Data;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(c =>
{
	c.AddPolicy("AllowOrigin", options => 
	options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddDbContext<Context>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("Online_storeContext")));

builder.Services.AddControllersWithViews()
	.AddNewtonsoftJson(options =>
		options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
	.AddNewtonsoftJson(options=>options.SerializerSettings.ContractResolver=new DefaultContractResolver());


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/login");
		options.AccessDeniedPath = new PathString("/Account/Login");
	});

builder.Services.AddControllers();
builder.Services.AddAuthorization();

var app = builder.Build();

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

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

app.Run();
