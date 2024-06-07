using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
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
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IBuyerService, BuyerService>();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OL-OASP_DEV_H-07_23.WebShop API", Version = "v1" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "OL-OASP_DEV_H-07_23.WebShop.xml");
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });

            });



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


            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Index}/{id?}");
            app.MapRazorPages();
            var identitySetup = app.Services.GetRequiredService<IIdentitySetup>();


            app.Run();
        }
    }
}
