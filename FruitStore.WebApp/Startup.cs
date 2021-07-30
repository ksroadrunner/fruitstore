using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

using Hangfire;
using Hangfire.SqlServer;
using Newtonsoft.Json;

using FruitStore.DataAccess;
namespace FruitStore.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add Redis cache!
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "127.0.0.1:6379";
            });

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    }));

            services.AddHangfireServer();
            services.AddControllersWithViews()
                    .AddNewtonsoftJson(config =>
                    {
                        // Disable notation ( Name = name )
                        config.SerializerSettings.ContractResolver = null;
                        config.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    });

            services.AddDbContext<DataContext>(ServiceLifetime.Transient);
            services.AddSession();

            services.AddRazorPages()
                    .AddRazorRuntimeCompilation();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<DataAccess.Services.IUserSession, SessionService>();
            services.AddScoped<DataAccess.Services.IUser, Business.BusUser>();
            services.AddScoped<DataAccess.Services.IFruitSeller, Business.BusFruitSeller>();

            //services.AddTransient
            //services.AddScoped
            //services.AddSingleton

            //Faz 2 Eğitim - https://docs.microsoft.com/en-US/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=visual-studio
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJob)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Security/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();
            app.UseHangfireServer(new BackgroundJobServerOptions()
            {
                SchedulePollingInterval = TimeSpan.FromMinutes(1),
                WorkerCount = System.Environment.ProcessorCount * 3
            });

            /* FireAndForget,
             * Delayed 
             * Recurring
             * Continuations */
            backgroundJob.Schedule(() => Console.WriteLine("123"), TimeSpan.FromSeconds(10));
            
            //backgroundJob.Schedule(() => Console.WriteLine("Selam"), TimeSpan.FromMinutes(30));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Security}/{action=Index}/{id?}");

                endpoints.MapHangfireDashboard("/Muhtar", new DashboardOptions()
                {
                    DashboardTitle = "Selam :)",
                    AppPath = "/Home/Index"
                });
            });
        }
    }
}