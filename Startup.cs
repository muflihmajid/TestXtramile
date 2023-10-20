using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using SceletonAPI.Application.Infrastructures;
using SceletonAPI.Application.Infrastructures.AutoMapper;
using SceletonAPI.Application.Interfaces;
using SceletonAPI.Application.Misc;
using SceletonAPI.Application.UseCases.RegisterUser;
using SceletonAPI.Infrastructure.Authorization;
using SceletonAPI.Infrastructure.ErrorHandler;
using SceletonAPI.Infrastructure.FileManager;
using SceletonAPI.Infrastructure.Notifications.Email;
using SceletonAPI.Infrastructure.Persistences;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SceletonAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // Add MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(RegisterUserCommandHandler)));
            // for handle midleware
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
            // add Context for access data
            services.AddTransient<IDBContext, DBContext>();
            // declare configurable data or 
            services.AddSingleton<Utils>();
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            // Add DbContext using SQL Server Provider
            services.AddDbContext<IDBContext, DBContext>(options =>
               options
               .UseLazyLoadingProxies()
               .UseSqlServer(Configuration.GetConnectionString("WADatabase")));
            // mapper
            var mappingConfig = AuthmapperFunction(services);
            services.AddSingleton(mappingConfig.CreateMapper());
            services
                .AddMvc()
                //.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()

                    };
                    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = c =>
                    {
                        var errors = string.Join('\n', c.ModelState.Values.Where(v => v.Errors.Count > 0)
                            .SelectMany(v => v.Errors)
                            .Select(v => v.ErrorMessage));
                        return new BadRequestObjectResult(new
                        {
                            ErrorCode = 400,
                            Message = errors
                        });
                    };
                });
            // services.AddSingleton<IEmailService, EmailService> ();
            services.AddScoped<IUploader, ManagedDiskUploader>();

            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }
        private MapperConfiguration AuthmapperFunction(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile(
                    services.BuildServiceProvider().GetService<IDBContext>(),
                    services.BuildServiceProvider().GetService<Utils>()
                ));
            });
            return mappingConfig;
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAll");
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/uploads"
            });
            app.UseMiddleware(typeof(ErrorHandlerMiddleware));
            app.UseAuthme();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}