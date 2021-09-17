namespace PrimeWeb1.Middleware
{
    public static class PrimeCheckerExtensions
    {
        public static IApplicationBuilder UsePrimeChecker(this IApplicationBuilder builder,
           PrimeCheckerOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            var primeService = builder.ApplicationServices.GetService(typeof(PrimeService)) as PrimeService;
#pragma warning disable CS8604 // Possible null reference argument.
            return builder.Use(next => new PrimeCheckerMiddleware(next, options, primeService).Invoke);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public static IApplicationBuilder UsePrimeChecker(this IApplicationBuilder builder,
            PathString path)
        {
            return UsePrimeChecker(builder, new PrimeCheckerOptions { Path = path });
        }
        public static IApplicationBuilder UsePrimeChecker(this IApplicationBuilder builder,
            string path)
        {
            return UsePrimeChecker(builder, new PrimeCheckerOptions { Path = new PathString(path) });
        }

        public static IApplicationBuilder UsePrimeChecker(this IApplicationBuilder builder)
        {
            return UsePrimeChecker(builder,
                new PrimeCheckerOptions()
                {
                    Path = new PathString("/checkprime")
                });
        }
    }
}
