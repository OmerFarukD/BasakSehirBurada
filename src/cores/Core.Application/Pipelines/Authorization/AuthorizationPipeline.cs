using Core.CrossCuttingConcerns.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Core.Application.Pipelines.Authorization;

public class AuthorizationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>, IRoleExists
{

    private IHttpContextAccessor _accessor;

    public AuthorizationPipeline(IHttpContextAccessor httpContext)
    {
        _accessor = httpContext;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var httpContext = _accessor.HttpContext;

        if (!httpContext.User.Identity.IsAuthenticated)
        {
            throw new AuthorizationException("Giriş Yapmadınız.");
        }

        var userClaims = httpContext.User.Claims.ToList();

        if(userClaims == null || !userClaims.Any())
        {
            throw new AuthorizationException("Yetkiniz yok");
        }


        var roles = httpContext.User.Claims
             .Where(x => x.Type == ClaimTypes.Role)
             .Select(x => x.Value)
             .ToList();

        bool isAuthorized = request.Roles.Any(role => roles.Contains(role));

        if(isAuthorized is false)
        {
            throw new AuthorizationException("Yetkiniz yok");
        }

        return await next();

    }
}
