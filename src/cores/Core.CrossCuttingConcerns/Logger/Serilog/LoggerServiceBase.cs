

using Serilog;

namespace Core.CrossCuttingConcerns.Logger.Serilog;

public class LoggerServiceBase 
{

    public LoggerServiceBase()
    {
        Logger = default;
    }

    protected ILogger Logger { get; set; }

    public LoggerServiceBase(ILogger logger)
    {
        Logger = logger;
    }



    public void Verbose(string message) => Logger.Verbose(message);
    public void Info(string message) => Logger.Information(message);
    public void Error(string message) => Logger.Error(message);
    public void Debug(string message) => Logger.Debug(message);
    public void Warning(string message) => Logger.Warning(message);

}
