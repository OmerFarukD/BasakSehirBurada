using Core.CrossCuttingConcerns.Logger.Models;
using Core.CrossCuttingConcerns.Logger.Serilog;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
namespace Core.Application.Pipelines.Loging;



public class LogingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ILoggableRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LoggerServiceBase _logger;

    public LogingPipeline(IHttpContextAccessor httpContextAccessor, LoggerServiceBase logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<LogParameter> parameters = new List<LogParameter>()
        {
            new LogParameter(){ Type = request.GetType().Name, Value = request}
        };

        var userInFo = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x=>x.Type== "Email").Value ?? "?";


        LogDetail logDetail = new LogDetail()
        {
            MethodName = next.Method.Name,
            Parameters = parameters,
            User = userInFo
        };
  

        string message = JsonSerializer.Serialize(logDetail);
        _logger.Info(message);

        return await next();

    }
}
