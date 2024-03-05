using Microsoft.EntityFrameworkCore;
using RegistrationAPI.Data;
using RegistrationAPI.Repositories;
using UserAuthentication.Repository.ExternalAPI;
using UserAuthentication.Repository.Interface;

namespace RegistrationAPI.Extension
{
    public static class  ExtensionMethod
    {
        public static void MyDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExternalCall, ExternalCall>();
            services.AddDbContext<AppDbContext>(o =>
            {
                o.UseInMemoryDatabase(configuration.GetConnectionString("MyConnection"));
            });
        }

    }
}
