using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtDemo.Shared.Dtos
{
    public class Response<TData> where TData : class
    {
        public TData Data { get; private set; }
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccessful { get; private set; }
        public ErrorDto Error { get; set; }

        public static Response<TData> Success(TData data, int statusCode)
        {
            return new Response<TData> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<TData> Success(int statusCode)
        {
            return new Response<TData> { Data = default, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<TData> Fail(ErrorDto errorDto, int statusCode)
        {
            return new Response<TData> { Error = errorDto, StatusCode = statusCode, IsSuccessful = false };
        }

        public static Response<TData> Fail(string errorMessage, int statusCode, bool isShow)
        {
            var errorDto = new ErrorDto(errorMessage, isShow);
            return new Response<TData> { Error = errorDto, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
