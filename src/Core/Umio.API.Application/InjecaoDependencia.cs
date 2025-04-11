using Microsoft.Extensions.DependencyInjection;
using Umio.API.Application.CasosDeUso.Enderecos;
using Umio.API.Application.CasosDeUso.Enderecos.Interfaces;
using Umio.API.Application.CasosDeUso.Usuarios;
using Umio.API.Application.CasosDeUso.Usuarios.Interfaces;

namespace Umio.API.Application
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection AdicionarAplicacao(this IServiceCollection services)
        {
            services.AddTransient<IBuscarEnderecoApiExterna, BuscarEnderecoApiExterna>();
            services.AddTransient<ICriarEndereco, CriarEndereco>();

            services.AddTransient<ICriarCliente, CriarCliente>();

            return services;
        }   
    }
}
