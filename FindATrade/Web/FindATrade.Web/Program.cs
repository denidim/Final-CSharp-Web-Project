namespace FindATrade.Web
{
    using System;
    using System.Reflection;

    using FindATrade.Data;
    using FindATrade.Data.Common;
    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Configurations;
    using FindATrade.Data.Models;
    using FindATrade.Data.Repositories;
    using FindATrade.Data.Seeding;
    using FindATrade.Services;
    using FindATrade.Services.Data;
    using FindATrade.Services.Mapping;
    using FindATrade.Services.Messaging;
    using FindATrade.Web.ViewModels;
    using Hangfire;
    using Hangfire.SqlServer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);
            var app = builder.Build();
            Configure(app);
            app.UseAuthentication();
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(connectionString));

            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true,
            }));

            services.AddHangfireServer();

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = new PathString("/AccountManagement/ManageAccount/LogIn");
            });

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            services.Configure<GCSConfigOptions>(configuration);

            services.AddSingleton(configuration);
            services.AddSingleton<ICloudStorageService, CloudStorageService>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<IGetCountsService, GetCountsService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ICompanyServiceService, CompanyServcieService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IVettingService, VettingService>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ISubscriptionService, SubscriptionService>();
            services.AddTransient<ILikeService, LikeService>();
            services.AddTransient<ISeederServcie, SeederService>();
        }

        private static void Configure(WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard();

            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
        }
    }
}
