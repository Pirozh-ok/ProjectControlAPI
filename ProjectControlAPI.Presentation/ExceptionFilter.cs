using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectControlAPI.Common.DTOs;
using ProjectControlAPI.Common.Exceptions;

namespace ProjectControlAPI.Presentation
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            int statusCode = 500;

            switch (context.Exception)
            {
                case NotFoundException:
                    {
                        statusCode = ((NotFoundException)context.Exception).ErrorCode;
                        break;
                    }
                case BadRequestException:
                    {
                        statusCode = ((BadRequestException)context.Exception).ErrorCode;
                        break;
                    }
                case AuthentificationException:
                    {
                        statusCode = ((AuthentificationException)context.Exception).ErrorCode;
                        break;
                    }
            }

            var exception = context.Exception;

            if (statusCode != 500)
            {
                var response = new ClientErrorResponse(statusCode.ToString(), exception.Message);

                context.HttpContext.Response.StatusCode = statusCode;
                context.Result = new JsonResult(response);
            }
            else
            {
                var response = new ServerErrorResponse(statusCode.ToString(), exception.Message, exception.StackTrace);

                context.HttpContext.Response.StatusCode = statusCode;
                context.Result = new JsonResult(response);
            }

            base.OnException(context);
        }
    }
}
