using Core.CrossCuttingConcerns.Exceptions;
using FluentValidation;
using MediatR;

namespace Core.Application.Pipelines.Validation;

public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest: IRequest<TResponse>

{

    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipeline(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        ValidationContext<object> context = new ValidationContext<object>(request);

        IEnumerable<ValidationExceptionModel> errors = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .GroupBy(
            p => p.PropertyName,
            (propName, errors) =>
                new ValidationExceptionModel()
                {
                    Property = propName,
                    Errors = errors.Select(x => x.ErrorMessage)
                }
            ).ToList();

        if (errors.Any())
        {
            throw new FluentValidationException(errors);
        }

        return await next();

    }
}
