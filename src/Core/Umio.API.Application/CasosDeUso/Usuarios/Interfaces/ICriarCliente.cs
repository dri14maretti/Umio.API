using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umio.API.Application.CasosDeUso.Usuarios.Interfaces
{
    public interface ICriarCliente
    {
        public Task<bool> Executar(string nome, string email, string telefone);
    }
}
