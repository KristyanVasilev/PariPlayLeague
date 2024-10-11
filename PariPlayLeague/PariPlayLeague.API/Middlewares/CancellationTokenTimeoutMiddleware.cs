namespace PariPlayLeague.API.Middlewares
{
    public class CancellationTokenTimeoutMiddleware
    {
        private readonly RequestDelegate _next;

        public CancellationTokenTimeoutMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(40));

            context.RequestAborted = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, context.RequestAborted).Token;

            await _next(context);
        }
    }
}
