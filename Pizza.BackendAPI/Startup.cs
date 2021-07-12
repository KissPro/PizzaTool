using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Pizza.Application.Approval;
using Pizza.Application.Assign;
using Pizza.Application.Config;
using Pizza.Application.Issue;
using Pizza.Application.Mail;
using Pizza.Data.EF;
using Pizza.Utilities.Exceptions;
using Pizza.ViewModel.Common;

namespace Pizza.BackendAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add config to common
            //services.Configure<ConfigModel>(Configuration.GetSection("ConfigModel"));
            //services.AddOptions();

            // Add connection string
            services.AddDbContext<PizzaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Pizza")));

            // Add service connection with database
            services.AddTransient<IIssueService, IssueService>();
            services.AddTransient<IConfigService, ConfigService>();
            services.AddTransient<IAssignService, AssignService>();
            services.AddTransient<IApprovalService, ApprovalService>();

            // Authorize config
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });

            // Add session config
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);//You can set Time   
            });

            // Hangfire service
            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseSqlServerStorage(Configuration.GetConnectionString("Pizza"))
                //.UseMemoryStorage()
            );
            services.AddHangfireServer();


            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    // Related Class Prevent
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    // Set time UTC -> Local (Same Angular - Browser)
                    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
                }
            );

            // Cors config
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                     builder =>
                                     {
                                         builder.WithOrigins("http://localhost:55500",
                                                             "http://pizza-dev.fushan.fihnbb.com")
                                         .AllowAnyHeader()
                                         .AllowAnyOrigin()
                                         .AllowAnyMethod();
                                     });
            });



            services.AddControllers();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager,
                IServiceProvider serviceProvider
        )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"UploadedFile")),
                RequestPath = new PathString("/UploadedFile")
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "UploadedFile")),
                RequestPath = "/UploadedFile"
            });

            app.UseCors(MyAllowSpecificOrigins);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // Hangfire
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                IsReadOnlyFunc = (DashboardContext context) => true,
                Authorization = new[] { new MyAuthorizationFilter() }
            });
            backgroundJobClient.Enqueue(() => Console.WriteLine("Hello, I am background job!")); // Run now
            //recurringJobManager.AddOrUpdate("Run", () => serviceProvider.GetService<IDeadlineService>().SendMail(), "* * * * *"); // Schedule
            //backgroundJobClient.Schedule(() => serviceProvider.GetService<IDeadlineService>().SendMail(), TimeSpan.FromSeconds(5));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}

public class MyAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();
        //check user admin
        //if (httpContext.Request.QueryString.ToString().Contains("admin"))
        //{
        //return true;
        //}
        // Allow all authenticated users to see the Dashboard (potentially dangerous).
        //return httpContext.User.Identity.IsAuthenticated;
        return true;
    }
}