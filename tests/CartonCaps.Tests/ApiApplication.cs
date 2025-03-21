using CartonCaps.Api.Contexts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class ApiApplication : WebApplicationFactory<Program>
{
    private readonly string _environment;

    public ApiApplication(string environment = "Development")
    {
        _environment = environment;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(_environment);
        
        builder.ConfigureServices(services =>
        {
            services.AddScoped(sp =>
            {
                // Replace SQL with in-memory database for tests
                return new DbContextOptionsBuilder<ReferralDb>()
                    .UseInMemoryDatabase("Tests")
                    .UseApplicationServiceProvider(sp)
                    .Options;
            });
        });

        return base.CreateHost(builder);
    }
}