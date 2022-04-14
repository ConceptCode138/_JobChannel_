using BLL.Interfaces;
using BO.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Créeation dictionnaire avec le Type de l'exception, et pour cet clé 
        /// une valeur qui va etre une Action (méthode) avec comme parametre ExceptionContext
        /// </summary>
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly ITraceService _traceService;
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;


        public ApiExceptionFilterAttribute(ITraceService traceService, ILogger<ApiExceptionFilterAttribute> logger)
        {

            _traceService = traceService;

            _logger = logger;

            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                //Enregistrement des exceptions
                { typeof(NotFoundException), HandleNotFoundException }, //SI une exception de type NotFoundException est levée ALORS je lance cette méthode - DAns le cas où l'on trouve pas une ressource
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        /// <summary>
        /// Handle the good exception for the context
        /// </summary>
        /// <param name="context">Context of exception</param>
        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                var iNotLoggedException = type as INotLoggedException;

                if (iNotLoggedException == null)
                {
                    _traceService.TraceOnServer(context.Exception.Message);
                }

                _exceptionHandlers[type].Invoke(context); //invoquation en réflexion , c'est une action qui prend en parametre un ExceptionContext
                return;
            }

            if (!context.ModelState.IsValid)   //dans le cas du choix d'utiliser les modeles de validations de microsoft DECONSEILLE car utlisation de decorateur sur les attributs.
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context); // sinon c'est 500 internal server error 
        }

        /// <summary>
        /// Handle a validation model exception 
        /// </summary>
        /// <param name="context">Context Exception</param>
        //private void HandleValidationException(ExceptionContext context)
        //{
        //    var exception = context.Exception as ValidationException;

        //    var details = new ValidationProblemDetails(exception?.Errors)
        //    {
        //        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        //    };

        //    context.Result = new BadRequestObjectResult(details);

        //    context.ExceptionHandled = true;
        //}

        /// <summary>
        /// Handle Invalid Model State exception 
        /// </summary>
        /// <param name="context">Context of exception</param>
        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handle a not found ressource exception 
        /// </summary>
        /// <param name="context">Context of exception</param>
        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception?.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handle for Unauthorized Access Exception
        /// </summary>
        /// <param name="context">Context of the exception</param>
        private void HandleUnauthorizedAccessException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handle a forbidden Access excception
        /// </summary>
        /// <param name="context">Context of the exception</param>
        private void HandleForbiddenAccessException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handle for an Unknow Exception
        /// </summary>
        /// <param name="context">Context of the exception</param>
        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            _logger.LogError(context.Exception.ToString());

            context.ExceptionHandled = true;
        }
    }
}
