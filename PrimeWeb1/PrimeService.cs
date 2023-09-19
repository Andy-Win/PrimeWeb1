namespace PrimeWeb1
{
    public class PrimeService
    {
        public void Configure(IApplicationBuilder app)
        {
#nullable disable
            app.Run(async (context) =>
            {
                if (context.Request.Path.Value.Contains("checkprime"))
                {
                    int numberToCheck;
                    try
                    {
                        numberToCheck = int.Parse(context.Request.QueryString.Value.Replace("?", ""));
                        var primeService = new PrimeService();
                        if (primeService.IsPrime(numberToCheck))
                        {
                            await context.Response.WriteAsync(numberToCheck + " is prime!");
                        }
                        else
                        {
                            await context.Response.WriteAsync(numberToCheck + " is NOT prime!");
                        }
                    }
                    catch
                    {
                        await context.Response.WriteAsync("Pass in a number to check in the form /checkprime?5");
                    }
                }
                else
                {
                    await context.Response.WriteAsync("Hello World! To check if a number is prime, provide URL of the form /checkprime?5");
                }
            });
        }
        public bool IsPrime(int candidate)
        {
            if (candidate < 2)
            {
                return false;
            }
            for (int divisor = 2; divisor <= Math.Sqrt(candidate); divisor++)
            {
                if (candidate % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
