using AdminMNS.WebApp.Data;
using AdminMNS.WebApp.Data.Entities;
using AdminMNS.WebApp.Options;
using AdminMNS.WebApp.Services;
using AdminMNS.WebApp.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminMNS.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            string? connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            using (ServiceProvider? scope = builder.Services.BuildServiceProvider())
            {
                using RoleManager<IdentityRole> roleManager = scope.GetRequiredService<RoleManager<IdentityRole>>();
                await AppDbContextSeed.SeedRolesAsync(roleManager);
            }

            builder.Services.AddOptions();
            builder.Services.Configure<MailOptions>(builder.Configuration.GetSection("Email"));

			builder.Services.AddScoped<IRoleService, RoleService>();
			builder.Services.AddScoped<IGraduatingClassService, GraduatingClassService>();
			builder.Services.AddScoped<IEmailSender, EmailSender>();
			builder.Services.AddScoped<IUserService, UserService>();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}