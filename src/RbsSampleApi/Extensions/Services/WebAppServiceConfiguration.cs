namespace RbsSampleApi.Extensions.Services;

using RbsSampleApi.Middleware;
using RbsSampleApi.Services;
using System.Text.Json.Serialization;
using Serilog;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sieve.Services;
using System.Reflection;

public static class WebAppServiceConfiguration
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton(Log.Logger);
        // TODO update CORS for your env
        builder.Services.AddCorsService("RbsSampleApiCorsPolicy", builder.Environment);
        builder.Services.OpenTelemetryRegistration("RbsSampleApi");
        builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
        // using Newtonsoft.Json to support PATCH docs since System.Text.Json does not support them https://github.com/dotnet/aspnetcore/issues/24333
        // if you are not using PatchDocs and would prefer to use System.Text.Json, you can remove The `AddNewtonSoftJson()` line
        builder.Services.AddControllers()
            .AddNewtonsoftJson()
            .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        builder.Services.AddApiVersioningExtension();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
        builder.Services.AddScoped<SieveProcessor>();

        builder.Services.AddMvc(options => options.Filters.Add<ErrorHandlerFilterAttribute>())
            .AddFluentValidation(cfg => { cfg.AutomaticValidationEnabled = false; });
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        builder.Services.AddHealthChecks();
        builder.Services.AddSwaggerExtension();
    }


}