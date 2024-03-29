namespace HackMe.Infrastructure.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 404)
            {
                context.Response.Redirect("/Error/NotFound");
            }
            else if (context.Response.StatusCode == 405)
            {
                // TODO
                context.Response.Redirect("/Tasks");
            }
            else if (context.Response.StatusCode == 500)
            {
                context.Response.Redirect("/Error/Unhandled");
            }
        }
    }
}
