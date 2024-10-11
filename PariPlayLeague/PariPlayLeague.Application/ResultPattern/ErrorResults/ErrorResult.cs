using Microsoft.AspNetCore.Http;

namespace PariPlayLeague.Application.ResultPattern.ErrorResults
{
    internal interface IErrorResult
    {
        string Message { get; }
        int StatusCode { get; }
    }

    public class ErrorResult : Result, IErrorResult
    {
        public ErrorResult(string message) : this(message, StatusCodes.Status500InternalServerError)
        {
        }

        public ErrorResult(string message, int code)
        {
            Message = message;
            Success = false;
            StatusCode = code;
        }

        public int StatusCode { get; }
    }

    public class ErrorResult<T> : Result<T>, IErrorResult
    {
        public ErrorResult(string message = "Internal Exception") : this(message, StatusCodes.Status500InternalServerError)
        {
        }

        public ErrorResult(string message, int code) : base(message, code, false)
        {
            Message = message;
            Success = false;
            StatusCode = code;
        }
    }
}
