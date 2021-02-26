using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Accounts.API.Application.Queries;
using Accounts.API.Application.Requests;
using Accounts.API.Application.Responses;
using Accounts.API.Application.Responses.AccountResponses;
using Accounts.Domain.AggregatesModel.AccountAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers
{
    [Route("api/v1/accounts")]
    [ApiController]
    public class AccountsController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IAccountQueries _queries;

        public AccountsController([NotNull] IMediator mediator, [NotNull] IAccountQueries queries)
        {
            _mediator = mediator;
            _queries = queries;
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
            // TODO: enable pagination and filter
            IEnumerable<AccountSummaryResponse> accounts = await _queries.GetAccountsAsync();
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
        public async Task<IActionResult> GetAccount(int id)
        {
            AccountResponse account = await _queries.GetAccountAsync(id);

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
        /// <param name="request">The account to create.</param>
        /// <returns>Details of the created account.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateAccountRequest request)
        {
            AccountResponse response = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetAccount), new { id = response.Id }, response);
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            // TODO
            throw new NotImplementedException();
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
