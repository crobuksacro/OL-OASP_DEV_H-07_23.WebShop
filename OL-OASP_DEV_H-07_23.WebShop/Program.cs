using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OL_OASP_DEV_H_07_23.WebShop.Data;
using OL_OASP_DEV_H_07_23.WebShop.Mapping;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.UserModel;
using OL_OASP_DEV_H_07_23.WebShop.Services.Implementations;
using OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;

namespace OL_OASP_DEV_H_07_23.WebShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.Configure<AppSettings>(builder.Configuration);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddSingleton<IIdentitySetup, IdentitySetup>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IBuyerService, BuyerService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ICommonService, CommonService>();
            builder.Services.AddSingleton<IHostedService, QueueProcessor>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddDefaultIdentity<ApplicationUser>(
                options => {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;


                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                }


                )
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add session support
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Add session middleware
            app.UseSession();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            var identitySetup = app.Services.GetRequiredService<IIdentitySetup>();


            app.Run();
        }
    }
}
