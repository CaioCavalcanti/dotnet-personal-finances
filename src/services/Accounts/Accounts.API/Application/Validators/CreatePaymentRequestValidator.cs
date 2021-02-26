using System.Linq;
using Accounts.API.Application.Requests;
using Accounts.Domain.AggregatesModel.PaymentAggregate;
using FluentValidation;

namespace Accounts.API.Application.Validators
{
    public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
    {
        public CreatePaymentRequestValidator()
        {
            RuleFor(p => p.AccountId).Must(BeAValidAccountId).WithMessage("Account id is not valid.");
            RuleFor(p => p.Counterparty).NotEmpty();
            RuleFor(p => p.Method).NotEmpty().Must(BeAValidPaymentMethod).WithMessage("Payment method is not valid.");
            RuleFor(p => p.Type).NotEmpty().Must(BeAValidPaymentType).WithMessage("Payment type is not valid.");
            RuleFor(p => p.Value).NotEqual(0);
        }

        private bool BeAValidAccountId(int id)
        {
            // TODO: account exists? should couple or not?
            return id > 0;
        }

        private bool BeAValidPaymentMethod(string paymentMethod)
        {
            return PaymentMethod.GetAll<PaymentMethod>()
                .Select(at => at.Name)
                .Contains(paymentMethod);
        }

        private bool BeAValidPaymentType(string paymentType)
        {
            return PaymentType.GetAll<PaymentType>()
                .Select(at => at.Name)
                .Contains(paymentType);
        }
    }
}