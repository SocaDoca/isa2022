<<<<<<< HEAD
using MedicApp.Utils;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Net;
using System.Text;
=======
﻿using MedicApp.Utils;
using System.Net;
using System.Text.Json;
>>>>>>> AccountRegistration

namespace MedicApp.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {

                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;

                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }


                var result = JsonSerializer.Serialize(new { message = error?.Message });

                await response.WriteAsync(result);
            }
        }
    }
}
