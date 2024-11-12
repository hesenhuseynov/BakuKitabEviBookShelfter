using System.Reflection.Metadata;
using BookShelfter.Application.Common;
using FluentValidation;
using MediatR;

namespace BookShelfter.Application.Validators;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
 where TResponse : new()
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();



        if (failures.Count != 0)
        {
            var response = new TResponse();

            var responseType = response.GetType();
            var successProperty = responseType.GetProperty("Success");
            var messageProperty = responseType.GetProperty("Message");
            var errorsProperty = responseType.GetProperty("Errors");


            if (successProperty != null && messageProperty != null && errorsProperty != null)
            {
                successProperty.SetValue(response, false);
                messageProperty.SetValue(response, "Validation failed");
                errorsProperty.SetValue(response, failures.Select(f => f.ErrorMessage).ToList());
            }

            return response;    



        }



        return await next();
    }

}
