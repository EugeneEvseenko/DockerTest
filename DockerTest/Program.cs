using DockerTest;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(hostContext.Configuration)
            .CreateLogger();
    })
    .UseSerilog()
    .Build();

host.Run();