namespace DakSite.Middleware
{
    public class RedirectToNextServerInDev
    {
        private const string NEXT_SERVER_DOMAIN = "http://localhost:3000";

        private readonly RequestDelegate _next;

        private static bool CheckPathNeedRedirect(string path)
        {
            // swagger 相关页面也需要排除
            List<string> excludePaths = new() { "/swagger", "/api" };
            bool needRedirect = !excludePaths.Any(p => path.StartsWith(p));

            return needRedirect;
        }

        public RedirectToNextServerInDev(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (CheckPathNeedRedirect(context.Request.Path))
            {
                string target = $"{NEXT_SERVER_DOMAIN}{context.Request.Path}";

                context.Response.Redirect(target);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
