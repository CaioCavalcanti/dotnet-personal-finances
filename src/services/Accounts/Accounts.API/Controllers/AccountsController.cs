using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.API.Application.Queries;
using Accounts.API.Application.Requests;
using Accounts.API.Application.Responses;
using Accounts.Domain.AggregatesModel.AccountAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Accounts.API.Controllers
{
    [Route("api/v1/accounts")]
    [ApiController]
    public class AccountsController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IAccountQueries _accountQueries;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IMediator mediator, IAccountQueries accountQueries, ILogger<AccountsController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _accountQueries = accountQueries ?? throw new ArgumentNullException(nameof(accountQueries));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets a summary of all accounts for the current user.
        /// </summary>
        /// <returns>List of accounts summary.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AccountSummaryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        // TODO: add global  response on swagger, so it doesn't need to be repeated
        public async Task<IActionResult> Get()
        {
            IEnumerable<AccountSummaryResponse> accounts = await _accountQueries.GetAccountsAsync();
            return Ok(accounts);
        }

        /// <summary>
        /// Get the details for an account with a given id.
        /// </summary>
        /// <param name="id">The account unique identifier.</param>
        /// <returns>Details of account.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResponse<Account>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            AccountResponse account = await _accountQueries.GetAccountAsync(id);

            // TODO: should this be an exception handled by the filter?
            if (account is null)
            {
                return NotFound<Account>();
            }

            return Ok(account);
        }

        /// <summary>
        /// Creates an account.
        /// </summary>
        /// <param name="createAccountRequest">The created account.</param>
        [HttpPost]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateAccountRequest createAccountRequest)
        {
            AccountResponse account = await _mediator.Send(createAccountRequest);
            return CreatedAtAction(nameof(Get), account);
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
