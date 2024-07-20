using Microsoft.EntityFrameworkCore;
using AdvC_Final.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using AdvC_Final.Areas.Account.Models;

namespace AdvC_Final
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddMemoryCache();
			builder.Services.AddSession();

			var connectionString = builder.Configuration.GetConnectionString("AccountLoginContextConnection") ?? throw new InvalidOperationException("Connection string 'AccountLoginContextConnection' not found.");

		    builder.Services.AddDbContext<AccountLoginContext>(options => options.UseSqlite(connectionString));

		    builder.Services.AddDefaultIdentity<AccountLoginUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AccountLoginContext>();

			builder.Services.AddRouting(options =>
			{
				options.LowercaseUrls = true;
				options.AppendTrailingSlash = true;
			});

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<PetsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PetsContext")));

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

			app.UseSession();

			app.MapRazorPages();

			app.MapAreaControllerRoute(
				name: "account",
				areaName: "Account",
				pattern: "Account/{controller=Home}/{action=Index}/{id?}");

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
