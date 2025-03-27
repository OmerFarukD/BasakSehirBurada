using Core.CrossCuttingConcerns.Logger.Serilog.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Core.CrossCuttingConcerns.Logger.Serilog;

public class FileLogger : LoggerServiceBase
{
    private readonly IConfiguration _configuration;

    public FileLogger(IConfiguration configuration)
    {
        _configuration = configuration;



        FileLogConfiguration logConfiguration = configuration.GetSection("SerilogLogConfigurations:FileLogConfiguration").Get<FileLogConfiguration>();


        string filepath = string.Format(format: "{0}{1}", arg0: Directory.GetCurrentDirectory() + logConfiguration.FolderPath, arg1: ".txt");

        Logger = new LoggerConfiguration().WriteTo.File(
            filepath,
            rollingInterval: RollingInterval.Day,
            retainedFileTimeLimit: null,
            fileSizeLimitBytes: 5000000,
             outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
            ).CreateLogger();
    }
}
