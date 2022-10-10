using Api;
using Serilog;

try
{
    await Host.
        CreateDefaultBuilder(args).
        ConfigureAppConfiguration((context, builder) =>
        {
            builder.Sources.Clear();

            builder.SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json", optional: false, true);

            if (!string.IsNullOrEmpty(context.HostingEnvironment.EnvironmentName))
                builder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, true);

            builder.AddEnvironmentVariables().AddCommandLine(args);
        }).
        ConfigureLogging((context, logging) =>
        {
            Log.Logger = new LoggerConfiguration().
                ReadFrom.
                Configuration(context.Configuration).
                Enrich.FromLogContext().
                CreateLogger();
        }).
        ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseContentRoot(Directory.GetCurrentDirectory()).UseStartup<Startup>();
        }).
        UseSerilog().
        Build().
        RunAsync();
}
catch (Exception exception)
{
    Log.Logger.Error(exception, "application error");
    throw;
}