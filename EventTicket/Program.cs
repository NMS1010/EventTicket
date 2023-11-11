using EventTicket.Repository.Category;
using EventTicket.Repository.DBContext;
using EventTicket.Repository.Event;
using EventTicket.Repository.Place;
using EventTicket.Repository.Topic;
using EventTicket.Repository.User;
using EventTicket.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext")));

builder.Services.AddControllersWithViews();
builder.Services
	.AddAuthentication("UserAuth")
	.AddCookie("UserAuth", options =>
	{
		options.LoginPath = "/member-login";
		options.ExpireTimeSpan = TimeSpan.FromDays(1);
		options.AccessDeniedPath = "/home";
		options.LogoutPath = "/logout";
		options.Cookie.Name = "User";
	});
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

builder.Services
	.AddScoped<ICategoryRepository, CategoryRepository>()
	.AddScoped<IPlaceRepository, PlaceRepository>()
	.AddScoped<ITopicRepository, TopicRepository>()
	.AddScoped<IEventRepository, EventRepository>()
	.AddScoped<IUserRepository, UserRepository>()
	.AddScoped<IUploadService, UploadService>();

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
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.UseCookiePolicy();
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
using (var scope = app.Services.CreateScope())
{
	using (var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
	{
		try
		{
			appContext.Database.Migrate();
		}
		catch (Exception ex)
		{
			throw;
		}
	}
}
app.Run();