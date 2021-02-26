using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Accounts.API.Application.Queries;
using Accounts.API.Application.Requests;
using Accounts.API.Application.Responses;
using Accounts.API.Application.Responses.PaymentResponses;
using Accounts.Domain.AggregatesModel.PaymentAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers
{
    [Route("api/v1/payments")]
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

        /// <summary>
        /// Creates a payment.
        /// </summary>
        /// <param name="request">The payment to create.</param>
        /// <returns>The created payment.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreatePaymentRequest request)
        {
            PaymentResponse response = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetPayment), new { id = response.Id }, response);
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
