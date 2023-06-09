using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DIConfiguration
{
    public static void RegisterData(this IServiceCollection services)
    {
        services.AddDbContext<BankContext>(options =>
            options.UseInMemoryDatabase("BankDb"));
    }
}
