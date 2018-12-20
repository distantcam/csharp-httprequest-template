using System;
using Function;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        app.Run(async (context) =>
        {
            try
            {
                await new FunctionHandler().Handle(context);
            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync(ex.Message);
            }
        });
    }
}