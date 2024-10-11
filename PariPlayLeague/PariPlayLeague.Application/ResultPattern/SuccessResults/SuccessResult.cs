namespace PariPlayLeague.Application.ResultPattern.SuccessResults
{
    public class SuccessResult : Result
    {
        public SuccessResult()
        {
            Success = true;
        }

        public SuccessResult(string message)
        {
            Success = true;
            Message = message;
        }
    }

    public class SuccessResult<T> : Result<T>
    {
        public SuccessResult(T data) : base(data)
        {
            Success = true;
            StatusCode = 200;
        }
    }
}
