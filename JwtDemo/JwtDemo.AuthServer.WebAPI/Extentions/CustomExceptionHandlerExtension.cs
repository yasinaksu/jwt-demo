using JwtDemo.Shared.Dtos;
using JwtDemo.Shared.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace JwtDemo.AuthServer.WebAPI.Extentions
{
    public static class CustomExceptionHandlerExtension
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(configure =>
            {
                configure.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (errorFeature !=null)
                    {
                        var exception = errorFeature.Error;
                        ErrorDto errorDto = null;
                        if (exception is CustomException)
                        {
                            errorDto = new ErrorDto(exception.Message, true);
                        }
                        else
                        {
                            errorDto = new ErrorDto(exception.Message, false);
                        }

                        var response = Response<NoDataDto>.Fail(errorDto, context.Response.StatusCode);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                });
            });
        }
    }
}
