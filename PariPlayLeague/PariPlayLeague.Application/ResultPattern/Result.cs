namespace PariPlayLeague.Application.ResultPattern
{
    public abstract class Result
    {
        public bool Success { get; protected set; }
        public string Message { get; set; } = string.Empty;
    }

    public abstract class Result<T> : Result
    {
        private T? _data;
        public int StatusCode { get; protected set; }
        protected Result(T data)
        {
            Data = data;
        }

        protected Result(string message, int code, bool success)
        {
            Message = message;
            StatusCode = code;
            Success = success;
        }

        public T? Data
        {
            get => Success ? _data ?? throw new Exception($"You can't access .{nameof(Data)} when .{nameof(Success)} is false") : default;
            set => _data = value;
        }
    }
}
