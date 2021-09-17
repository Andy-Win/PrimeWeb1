namespace PrimeWeb1.Middleware
{
    public class PrimeCheckerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly PrimeCheckerOptions _options;
        private readonly PrimeService _primeService;

        public PrimeCheckerMiddleware(RequestDelegate next,
            PrimeCheckerOptions options,
            PrimeService primeService)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (primeService == null)
            {
                throw new ArgumentNullException(nameof(primeService));
            }

            _next = next;
            _options = options;
            _primeService = primeService;
        }

        public async Task Invoke(HttpContext context)
        {
            HttpRequest request = context.Request;
            if (!request.Path.HasValue ||
                request.Path != _options.Path)
            {
                await context.Response.WriteAsync($"Hello World! To check if a number is prime, provide URL of the form {_options.Path}?5");
                // await _next.Invoke(context);
            }
            else
            {
                int numberToCheck;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (int.TryParse(request.QueryString.Value.Replace("?", ""), out numberToCheck))
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                {
                    if (_primeService.IsPrime(numberToCheck))
                    {
                        await context.Response.WriteAsync($"{numberToCheck} is prime!");
                    }
                    else
                    {
                        await context.Response.WriteAsync($"{numberToCheck} is NOT prime!");
                    }
                }
                else
                {
                    await context.Response.WriteAsync($"Pass in a number to check in the form {_options.Path}?5");
                }
            }
        }
    }
}
