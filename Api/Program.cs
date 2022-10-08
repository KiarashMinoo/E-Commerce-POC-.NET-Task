using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Prometheus;
using Serilog;
using System.IO.Compression;
using Infrastructure.DependencyInjection;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.
        ConfigureAppConfiguration((context, builder) =>
        {
            builder.Sources.Clear();

            builder.SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json", optional: false, true);

            if (!string.IsNullOrEmpty(context.HostingEnvironment.EnvironmentName))
                builder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, true);

            builder.AddEnvironmentVariables().AddCommandLine(args);
        });

    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

    // Add services to the container.
    builder.Services.
        AddServices()
        AddDbServices().
        AddLogging(options => options.AddSerilog(dispose: true)).
        Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; }).
        Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; }).
        AddResponseCompression(options =>
        {
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        }).
        AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.WithOrigins("http://localhost:4200", "https://localhost:4200", "*")
                    .AllowCredentials() //Note:  The URL must be specified without a trailing slash (/).
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true));
        }).
        AddHttpContextAccessor().
        AddControllers().
        AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Formatting = Formatting.None;
            options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.ObjectCreationHandling = ObjectCreationHandling.Replace;
            options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            options.SerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
        }).Services.
        AddHealthChecks().
        AddNpgSql(builder.Configuration.GetConnectionString("PostgreSql")).
        AddMongoDb(builder.Configuration["MongoDb:ConnectionString"]).
        AddDiskStorageHealthCheck(null).
        AddPrivateMemoryHealthCheck(367001600).
        AddProcessAllocatedMemoryHealthCheck(1).
        ForwardToPrometheus();

    if (builder.Environment.IsDevelopment())
    {
        builder.Services.
            AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = HeaderNames.Authorization,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
                });

                Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).
                    ToList().
                    ForEach(xml => options.IncludeXmlComments(xml, includeControllerXmlComments: true));
            }).
            AddSwaggerGenNewtonsoftSupport();
    }

    var app = builder.Build();

    app.Services.GetService<ILoggerFactory>().AddSerilog();

    var appLifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
    appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);

    app.UseSerilogRequestLogging(options =>
    {
        options.EnrichDiagnosticContext = (IDiagnosticContext diagnosticContext, HttpContext httpContext) =>
        {
            var userId = httpContext.User.FindFirst("sid")?.Value;
            if (!string.IsNullOrEmpty(userId))
                diagnosticContext.Set("UserId", userId);
        };
    });

    if (builder.Environment.IsDevelopment())
    {
        app.UseSwagger(c =>
        {
            c.SerializeAsV2 = true;
            c.RouteTemplate = "swagger/{documentName}/swagger.json";
        }).
        UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("v1/swagger.json", "E-Commerce API V1");
        });
    }

    app.UseResponseCompression().
        UseAuthentication().
        UseRouting().
        UseCors("CorsPolicy").
        UseAuthorization().
        UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapHealthChecks("/api/health", new HealthCheckOptions()
            {
                ResultStatusCodes = { [HealthStatus.Healthy] = StatusCodes.Status200OK, [HealthStatus.Degraded] = StatusCodes.Status200OK, [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable }
            });
        });

    app.Run();

}
catch (Exception exception)
{
    Log.Logger.Error(exception, "application error");
    throw;
}