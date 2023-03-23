using PuppeteerSharp;

namespace DockerTest;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            if (Directory.Exists("/usr/bin"))
            {
                foreach (var file in new DirectoryInfo("/usr/bin").GetFiles())
                {
                    _logger.LogInformation(file?.FullName);
                }
            }else
                _logger.LogError("Not found /usr/bin");
            
            if (Directory.Exists("/usr/bin/google-chrome"))
            {
                foreach (var file in new DirectoryInfo("/usr/bin/google-chrome").GetFiles())
                {
                    _logger.LogInformation(file?.FullName);
                }
            }else
                _logger.LogError("Not found /usr/bin/google-chrome");
            
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = "/usr/bin/google-chrome-stable"
            });
            
            var page = await browser.NewPageAsync();
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = 1920,
                Height = 1080
            });
            await page.GoToAsync("http://www.google.com");
            var screenshot = await page.ScreenshotDataAsync();
            
            if (!Directory.Exists("/usr/images"))
                Directory.CreateDirectory("/usr/images");
            
            File.WriteAllBytes($"/usr/images/{new Random().Next()}.jpg", screenshot);
            foreach (var file in new DirectoryInfo("/usr/images").GetFiles())
            {
                _logger.LogInformation(file?.FullName);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}