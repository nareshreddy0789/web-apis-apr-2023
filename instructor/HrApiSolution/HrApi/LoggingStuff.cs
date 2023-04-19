namespace HrApi;

public static class LoggingStuff
{
    public static async Task LogIt(HttpContext context, Func<Task> next)
    {
        await Console.Out.WriteLineAsync($"Just got a request from {context.Request.Headers.UserAgent}");
        await next();
    } 

    public static IApplicationBuilder UseSuperLogging(this IApplicationBuilder app)
    {
        app.Use(LogIt);
        return app;
    }
}
