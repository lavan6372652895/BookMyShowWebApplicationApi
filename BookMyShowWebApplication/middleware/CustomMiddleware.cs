namespace BookMyShowWebApplication.middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILoggerManager _logger;
        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public void Invoke(HttpContext context)
        {

        }

    }
}
