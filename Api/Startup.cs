using Api.Controllers;
using Application.Services.Token;
using Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Prometheus;
using Serilog;
using System.IO.Compression;
using System.Net;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Api
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var jwtConfiguration = new JwtConfiguration();
            configuration.GetSection("Jwt").Bind(jwtConfiguration);

            services.
                AddServices().
                AddDbServices(configuration, "PostgreSql", "MongoDb").
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
                        builder => builder
                            .AllowCredentials() //Note:  The URL must be specified without a trailing slash (/).
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .SetIsOriginAllowed((host) => true));
                }).
                AddHttpContextAccessor().
                AddSingleton(jwtConfiguration).
                AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).
                AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfiguration.Issuer,
                        ValidAudience = jwtConfiguration.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key))
                    };

                    options.Challenge = "token is required";
                });

            services.
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
                });

            services.
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

                    options.MapType(typeof(IFormFile), () => new OpenApiSchema() { Type = "file", Format = "binary" });
                    options.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Format = "date" });

                    Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).
                        ToList().
                        ForEach(xml => options.IncludeXmlComments(xml, includeControllerXmlComments: true));
                }).
                AddSwaggerGenNewtonsoftSupport().
                AddHealthChecks().
                AddNpgSql(configuration.GetConnectionString("PostgreSql")).
                AddMongoDb(configuration["MongoDb:ConnectionString"]).
                AddDiskStorageHealthCheck(null).
                AddPrivateMemoryHealthCheck(367001600).
                AddProcessAllocatedMemoryHealthCheck(367001600).
                ForwardToPrometheus();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.ApplicationServices.GetRequiredService<ILoggerFactory>().AddSerilog();
            app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>().ApplicationStopped.Register(Log.CloseAndFlush);

            app.
                UseDbServices().
                UseSerilogRequestLogging(options =>
                {
                    options.EnrichDiagnosticContext = (IDiagnosticContext diagnosticContext, HttpContext httpContext) =>
                    {
                        var userId = httpContext.User.FindFirst(ClaimTypes.Sid)?.Value;
                        if (!string.IsNullOrEmpty(userId))
                            diagnosticContext.Set("UserId", userId);
                    };
                }).
                UseSwagger(c =>
                {
                    c.SerializeAsV2 = true;
                    c.RouteTemplate = "swagger/{documentName}/swagger.json";
                }).
                UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "E-Commerce API V1");
                }).
                UseResponseCompression().
                UseFileServer(new FileServerOptions()
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                    RequestPath = new PathString("/wwwroot")
                }).
                UseFileServer(new FileServerOptions()
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                    RequestPath = new PathString("/_ui/wwwroot")
                }).
                UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new EmbeddedFileProvider(typeof(ProductController).Assembly, "Api.wwwroot"),
                    RequestPath = "/_ui",
                    OnPrepareResponse = ctx =>
                    {
                        ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age=604800");
                    }
                }).
                UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new EmbeddedFileProvider(typeof(ProductController).Assembly, "Api.wwwroot.assets.dist"),
                    RequestPath = "/assets/dist",
                    OnPrepareResponse = ctx =>
                    {
                        ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age=604800");
                    }
                }).
                UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new EmbeddedFileProvider(typeof(ProductController).Assembly, "Api.wwwroot.assets.dist"),
                    RequestPath = "/_framework",
                    OnPrepareResponse = ctx =>
                    {
                        ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age=604800");
                    }
                }).
                UseAuthentication().
                UseRouting().
                UseCors("CorsPolicy").
                UseAuthorization().
                UseExceptionHandler(exceptionHandlerApp =>
                {
                    exceptionHandlerApp.Run(async context =>
                    {
                        context.Response.ContentType = Text.Plain;

                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error is HttpRequestException httpRequestException)
                        {
                            context.Response.StatusCode = (int)(httpRequestException.StatusCode ?? HttpStatusCode.InternalServerError);
                            await context.Response.WriteAsync(httpRequestException.Message);
                        }
                        else
                        {
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            await context.Response.WriteAsync("An exception was thrown.");
                        }
                    });
                }).
                UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();

                    endpoints.MapHealthChecks("/api/health", new HealthCheckOptions()
                    {
                        ResultStatusCodes = { [HealthStatus.Healthy] = StatusCodes.Status200OK, [HealthStatus.Degraded] = StatusCodes.Status200OK, [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable }
                    });
                });

            Console.WriteLine("Application Started");
        }
    }
}
