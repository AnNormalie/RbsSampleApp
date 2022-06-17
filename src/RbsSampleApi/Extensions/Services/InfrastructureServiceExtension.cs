namespace RbsSampleApi.Extensions.Services;

using RbsSampleApi.Databases;
using RbsSampleApi.Resources;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {

        var dbConnectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<RbsDbContext>(
            options => options.UseSqlServer(
                dbConnectionString,
                builder =>
                {
                    builder.MigrationsAssembly(typeof(RbsDbContext).Assembly.FullName);
                }
        ));
    }
}
