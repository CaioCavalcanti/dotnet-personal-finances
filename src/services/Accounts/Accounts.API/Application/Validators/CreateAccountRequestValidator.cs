using System.Data;
using Accounts.API.Application.Requests;
using FluentValidation;

namespace Accounts.API.Application.Validators
{
    public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
    {
        public CreateAccountRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty();
            RuleFor(request => request.Type).NotEmpty();
            RuleFor(request => request.Currency).NotEmpty();
            RuleFor(request => request.InitialBalance).NotEmpty();
        }
    }
}