using Microsoft.Extensions.DependencyInjection;
using Umio.API.Application.Contratos.Servicos;
using Umio.API.ViaCepService.Services;

namespace Umio.API.ViaCepService
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection AdicionarViaCepService(this IServiceCollection services)
        {
            services.AddTransient<ICepService, CepService>();

            return services;
        }
    }
}
