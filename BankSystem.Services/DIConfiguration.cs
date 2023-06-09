using Microsoft.Extensions.DependencyInjection;

namespace BankSystem.Services
{
    public static class DIConfiguration 
    {
        public static void RegisterBusiness(this IServiceCollection services)
        {
            services.RegisterData();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
