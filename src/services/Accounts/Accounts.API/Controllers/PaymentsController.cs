using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Accounts.API.Application.Queries;
using Accounts.API.Application.Responses;
using Accounts.API.Application.Responses.PaymentResponses;
using Accounts.Domain.AggregatesModel.PaymentAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IPaymentQueries _queries;

        public PaymentsController([NotNull] IMediator mediator, [NotNull] IPaymentQueries queries)
        {
            _mediator = mediator;
            _queries = queries;
        }

        /// <summary>
        /// Gets a summary of all payments for the current user.
        /// </summary>
        /// <returns>List of payments summary.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PaymentSummaryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            // TODO: enable pagination and filter
            IEnumerable<PaymentSummaryResponse> payments = await _queries.GetPaymentsAsync();
            return Ok(payments);
        }

        /// <summary>
        /// Get the details of a payment with a given id.
        /// </summary>
        /// <param name="id">The payment unique identifier.</param>
        /// <returns>Details of the payment.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResponse<Payment>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPayment(int id)
        {
            PaymentResponse payment = await _queries.GetPaymentAsync(id);

            if(payment is null) {
                return NotFound<Payment>();
            }

            return Ok(payment);
        }

        // POST: api/Payments
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
