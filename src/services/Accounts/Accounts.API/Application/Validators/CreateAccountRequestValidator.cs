using System.Linq;
using System.Data;
using Accounts.API.Application.Requests;
using Accounts.Domain.AggregatesModel.AccountAggregate;
using FluentValidation;

namespace Accounts.API.Application.Validators
{
    public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
    {
        public CreateAccountRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty();
            RuleFor(request => request.Type).NotEmpty().Must(BeAValidAccountType).WithMessage("Account type is not valid.");
            RuleFor(request => request.Currency).NotEmpty();
        }

        private bool BeAValidAccountType(string accountType)
        {
            return AccountType.GetPredefinedAccountTypes()
                .Select(at => at.Name)
                .Contains(accountType);
        }
    }
}