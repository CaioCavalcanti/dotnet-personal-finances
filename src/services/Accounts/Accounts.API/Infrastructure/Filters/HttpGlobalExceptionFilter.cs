using System;
using Accounts.API.Application.Responses;
using Accounts.API.Infrastructure.ActionResults;
using Accounts.Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Accounts.API.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment hostingEnvironment, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void OnException(ExceptionContext context)
        {
            string correlationId = Guid.NewGuid().ToString();

            LogError(context, correlationId);

            if (context.Exception.GetType() == typeof(AccountsDomainException))
            {
                SetBadRequestResponse(context);
            }
            else
            {
                SetInternalServerErrorResponse(context, correlationId);
            }

            context.ExceptionHandled = true;
        }

        private void LogError(ExceptionContext context, string correlationId)
        {
            _logger.LogError(
                new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message,
                correlationId
            );
        }

        private void SetBadRequestResponse(ExceptionContext context)
        {
            ValidationProblemDetails response = CreateValidationProblemDetails(context);

            context.Result = new BadRequestObjectResult(response);
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }

        private ValidationProblemDetails CreateValidationProblemDetails(ExceptionContext context)
        {
            var problemDetails = new ValidationProblemDetails
            {
                Instance = context.HttpContext.Request.Path,
                Status = StatusCodes.Status400BadRequest
            };

            // TODO: write down the errors from inner exception
            problemDetails.Errors.Add("DomainValidations", new string[] { context.Exception.Message.ToString() });

            return problemDetails;
        }

        private void SetInternalServerErrorResponse(ExceptionContext context, string correlationId)
        {
            ErrorResponse response = CreateErrorResponse(context, correlationId);

            context.Result = new InternalServerErrorObjectResult(response);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        private ErrorResponse CreateErrorResponse(ExceptionContext context, string correlationId)
        {
            var response = new ErrorResponse(correlationId, "An error occurred.");

            if (_hostingEnvironment.IsDevelopment())
            {
                response.AddExceptionDetails(context.Exception);
            }

            return response;
        }
    }
}