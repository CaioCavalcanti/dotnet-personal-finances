using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Accounts.Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Accounts.API.Application.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest>[] _validators;

        public ValidatorBehavior([NotNull] IValidator<TRequest>[] validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!TryValidateRequest(request, out IEnumerable<ValidationFailure> errors))
            {
                ThrowValidationException(errors);
            }

            return await next();
        }

        private bool TryValidateRequest(TRequest request, out IEnumerable<ValidationFailure> errors)
        {
            errors = _validators
                .Select(validator => validator.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            return !errors.Any();
        }

        private void ThrowValidationException(IEnumerable<ValidationFailure> errors)
        {
            var message = $"Request is not valid for type {typeof(TRequest).Name}.";
            var innerException = new ValidationException("Validation exception.", errors);
            throw new AccountsDomainException(message, innerException);
        }
    }
}