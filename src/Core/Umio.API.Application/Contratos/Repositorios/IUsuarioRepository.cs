using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umio.API.Application.CasosDeUso.Clientes.Inputs;

namespace Umio.API.Application.Contratos.Repositorios
{
    public interface IUsuarioRepository
    {
        Task<bool> CriarUsuario(Guid clienteId, string senha, string provedor);
        Task<bool> DeletarPorClienteId(Guid clienteId);
    }
}