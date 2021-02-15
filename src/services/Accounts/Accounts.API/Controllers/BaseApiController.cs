using Accounts.API.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected NotFoundObjectResult NotFound<T>() where T : class
        {
            return NotFound(new NotFoundResponse<T>());
        }
    }
}