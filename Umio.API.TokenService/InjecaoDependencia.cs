using Microsoft.Extensions.DependencyInjection;
using Umio.API.Application.Contratos.Servicos;
using Umio.API.TokenService.Models;
using Umio.API.TokenService.Services;

namespace Umio.API.TokenService
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection AdicionarTokenService(this IServiceCollection services, ConfiguracoesJwt configuracoesJwt)
        {
            services.AddTransient<ITokenService, TokenGenerator>();
            services.AddSingleton(configuracoesJwt);

            return services;
        }
    }
}
