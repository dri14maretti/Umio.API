using Microsoft.Extensions.DependencyInjection;
using Umio.API.Application.CasosDeUso.Enderecos;
using Umio.API.Application.CasosDeUso.Enderecos.Interfaces;
using Umio.API.Application.CasosDeUso.Clientes;
using Umio.API.Application.CasosDeUso.Clientes.Interfaces;
using Umio.API.Application.CasosDeUso.Usuarios.Interfaces;
using Umio.API.Application.CasosDeUso.Usuarios;

namespace Umio.API.Application
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection AdicionarAplicacao(this IServiceCollection services)
        {
            services.AddTransient<IBuscarEnderecoApiExterna, BuscarEnderecoApiExterna>();
            services.AddTransient<ICriarEndereco, CriarEndereco>();
            services.AddTransient<IBuscarEnderecosCliente, BuscarEnderecosCliente>();
            services.AddTransient<IExcluirEndereco, ExcluirEndereco>();

            services.AddTransient<ICriarCliente, CriarCliente>();
            services.AddScoped<IAtualizarCliente, AtualizarCliente>();
            services.AddScoped<IDeletarCliente, DeletarCliente>();
            services.AddScoped<IListarCliente, ListarCliente>();

            services.AddTransient<ILoginUsuario, LoginUsuario>();

            return services;
        }   
    }
}
