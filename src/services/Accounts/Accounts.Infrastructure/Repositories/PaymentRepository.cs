using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Accounts.Domain.AggregatesModel.PaymentAggregate;
using Accounts.Domain.SeedWork;
using Accounts.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AccountsDbContext _context;

        public PaymentRepository([NotNull] AccountsDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Payment Add(Payment payment)
        {
            return _context.Payments.Add(payment).Entity;
        }

        public Task<Payment> GetAsync(int id)
        {
            return _context.Payments.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Payment>> GetAsync()
        {
            return await _context.Payments
                .Include(a => a.Type)
                .Include(p => p.Method)
                .ToListAsync();
        }

        public void Update(Payment payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
        }
    }
}