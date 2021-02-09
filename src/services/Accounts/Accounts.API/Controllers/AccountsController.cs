using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.API.Application.Queries;
using Accounts.API.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Accounts.API.Controllers
{
    [Route("api/v1/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
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
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AccountSummaryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<AccountSummaryResponse> accounts = await _accountQueries.GetAccountsAsync();
            return Ok(accounts);
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            AccountResponse account = await _accountQueries.GetAccountAsync(id);
            return Ok(account);
        }

        // POST: api/Accounts
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
