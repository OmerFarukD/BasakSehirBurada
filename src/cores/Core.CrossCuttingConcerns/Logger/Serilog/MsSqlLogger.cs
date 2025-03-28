using Core.CrossCuttingConcerns.Logger.Serilog.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Core.CrossCuttingConcerns.Logger.Serilog;

public class MsSqlLogger : LoggerServiceBase
{

    public MsSqlLogger(IConfiguration configuration)
    {
        MsSqlConfiguration logConfig = configuration.GetSection("SerilogLogConfigurations:MsSqlConfiguration").Get<MsSqlConfiguration>();

        MSSqlServerSinkOptions options = new MSSqlServerSinkOptions()
        {
            TableName = logConfig.TableName,
            AutoCreateSqlTable = logConfig.AutoCreateSqlTable
        };

        var columnOptions = new ColumnOptions();

        global::Serilog.Core.Logger serilogConfig = new LoggerConfiguration().WriteTo
            .MSSqlServer(logConfig.ConnectionString, sinkOptions:options,columnOptions:columnOptions)
            .CreateLogger();


        Logger = serilogConfig;
    }
}
