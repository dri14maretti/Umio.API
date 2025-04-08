using Microsoft.Extensions.DependencyInjection;
using Umio.API.Application.CasosDeUso.Enderecos;
using Umio.API.Application.CasosDeUso.Enderecos.Interfaces;

namespace Umio.API.Application
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection AdicionarAplicacao(this IServiceCollection services)
        {
            services.AddTransient<IBuscarEnderecoApiExterna, BuscarEnderecoApiExterna>();

            return services;
        }   
    }
}
