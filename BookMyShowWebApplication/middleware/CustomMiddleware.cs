namespace BookMyShowWebApplication.middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public void Invoke(HttpContext context)
        {

        }

    }
}
