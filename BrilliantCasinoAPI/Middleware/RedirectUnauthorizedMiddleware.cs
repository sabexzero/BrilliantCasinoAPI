
namespace BrilliantCasinoAPI.Middleware;
public class RedirectUnauthorizedMiddleware
{
    private readonly RequestDelegate _next;

    public RedirectUnauthorizedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            // Перенаправление на нужный маршрут после неуспешной авторизации
            context.Response.Redirect("/Loshara");
            return;
        }

        await _next(context);
    }
}