using Microsoft.Extensions.DependencyInjection;
using Umio.API.Application.Contratos.Repositorios;

namespace Umio.API.Postgres
{
    public static class InjecaoDependecia
    {
        public static IServiceCollection AdicionarPostgres(this IServiceCollection services)
        {
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IEnderecoRepository, EnderecoRepository>();

            return services;
        }
    }
}
