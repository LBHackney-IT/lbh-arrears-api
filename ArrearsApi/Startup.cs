using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ArrearsApi.V1.Controllers;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using ArrearsApi.V1.Gateways;
using ArrearsApi.V1.Infrastructure;
using ArrearsApi.V1.UseCase;
using ArrearsApi.V1.UseCase.Interfaces;
using ArrearsApi.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
//using Hackney.Core.Logging;
//using Hackney.Core.Middleware.Logging;
//using Microsoft.AspNetCore.Diagnostics.HealthChecks;
//using Hackney.Core.HealthCheck;
//using Hackney.Core.Middleware.CorrelationId;
//using Hackney.Core.DynamoDb.HealthCheck;
//using Hackney.Core.DynamoDb;
using Hackney.Core.Middleware.Exception;

namespace ArrearsApi
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            AWSSDKHandler.RegisterXRayForAllServices();
        }

        public IConfiguration Configuration { get; }
        private static List<ApiVersionDescription> _apiVersions { get; set; }

        private const string ApiName = "Arrears API";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true; // assume that the caller wants the default version if they don't specify
                o.ApiVersionReader = new UrlSegmentApiVersionReader(); // read the version number from the url segment header)
            });

            services.AddSingleton<IApiVersionDescriptionProvider, DefaultApiVersionDescriptionProvider>();

            //services.AddDynamoDbHealthCheck<EvictionsEntity>();

            services.AddSwaggerGen(c =>
            {
                //c.AddSecurityDefinition("Token",
                //    new OpenApiSecurityScheme
                //    {
                //        In = ParameterLocation.Header,
                //        Description = "Your Hackney API Key",
                //        Name = "X-Api-Key",
                //        Type = SecuritySchemeType.ApiKey
                //    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Token" }
                        },
                        new List<string>()
                    }
                });

                //Looks at the APIVersionAttribute [ApiVersion("x")] on controllers and decides whether or not
                //to include it in that version of the swagger document
                //Controllers must have this [ApiVersion("x")] to be included in swagger documentation!!
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    apiDesc.TryGetMethodInfo(out var methodInfo);

                    var versions = methodInfo?
                        .DeclaringType?.GetCustomAttributes()
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions).ToList();

                    return versions?.Any(v => $"{v.GetFormattedApiVersion()}" == docName) ?? false;
                });

                //Get every ApiVersion attribute specified and create swagger docs for them
                foreach (var apiVersion in _apiVersions)
                {
                    var version = $"v{apiVersion.ApiVersion.ToString()}";
                    c.SwaggerDoc(version, new OpenApiInfo
                    {
                        Title = $"{ApiName}-api {version}",
                        Version = version,
                        Description = $"{ApiName} version {version}. Please check older versions for depreciated endpoints."
                    });
                }

                c.CustomSchemaIds(x => x.FullName);
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                    c.IncludeXmlComments(xmlPath);
            });

            //services.ConfigureLambdaLogging(Configuration);

            //services.AddLogCallAspect();

            ConfigureDbContext(services);

            RegisterGateways(services);
            RegisterUseCases(services);
        }

        private static void ConfigureDbContext(IServiceCollection services)
        {
            var connectionStringInterimSolution = Environment.GetEnvironmentVariable("CONNECTION_STRING_INTERIM_SOLUTION");
            services.AddDbContext<InterimSolutionContext>(
                opt => opt.UseSqlServer(connectionStringInterimSolution).AddXRayInterceptor(true));

            var connectionStringIncome = Environment.GetEnvironmentVariable("CONNECTION_STRING_INCOME");
            services.AddDbContext<IncomeContext>(
                opt => opt.UseMySQL(connectionStringIncome).AddXRayInterceptor(true));
        }



        private static void RegisterGateways(IServiceCollection services)
        {
            services.AddScoped<IBatchLogGateway, BatchLogGateway>();
            services.AddScoped<IEvictionsGateway, EvictionsGateway>();
        }

        private static void RegisterUseCases(IServiceCollection services)
        {
            services.AddScoped<IGetAllBatchLogUseCase, GetAllBatchLogUseCase>();
            services.AddScoped<IGetAllEvictionsUseCase, GetAllEvictionsUseCase>();
            services.AddScoped<IGetBatchLogByIdUseCase, GetBatchLogByIdUseCase>();
            services.AddScoped<IGetEvictionsByIdUseCase, GetEvictionsByIdUseCase>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.UseCors(builder => builder
                  .AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .WithExposedHeaders("x-correlation-id"));

            //app.UseCorrelationId();
            //app.UseLoggingScope();
            //app.UseCustomExceptionHandler(logger);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseXRay("lbh-arrears-api");


            //Get All ApiVersions,
            var api = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
            _apiVersions = api.ApiVersionDescriptions.ToList();

            //Swagger ui to view the swagger.json file
            app.UseSwaggerUI(c =>
            {
                foreach (var apiVersionDescription in _apiVersions)
                {
                    //Create a swagger endpoint for each swagger version
                    c.SwaggerEndpoint($"{apiVersionDescription.GetFormattedApiVersion()}/swagger.json",
                        $"{ApiName}-api {apiVersionDescription.GetFormattedApiVersion()}");
                }
            });
            app.UseSwagger();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                // SwaggerGen won't find controllers that are routed via this technique.
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapHealthChecks("/api/v1/healthcheck/ping", new HealthCheckOptions()
                //{
                //    ResponseWriter = HealthCheckResponseWriter.WriteResponse
                //});
            });
            //app.UseLogCall();
        }
    }
}
