using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Postgres
{
    class EnderecoRepository : IEnderecoRepository
    {
        public async Task<Endereco> BuscarEnderecoPorId(Guid id)
        {
            return Endereco.CriarNovoEndereco(
                    "37701240",
                    "Rua Doutor Domiciano Costa Moreira",
                    "Pinheirinho",
                    "Poços de Caldas",
                    "MG",
                    210,
                    "APTO. 7",
                    Guid.NewGuid()
            );
        }

        public async Task<IEnumerable<Endereco>> BuscarEnderecosCliente(Guid clienteId)
        {
            return new List<Endereco>()
            {
                Endereco.CriarNovoEndereco(
                    "37701240",
                    "Rua Doutor Domiciano Costa Moreira",
                    "Pinheirinho",
                    "Poços de Caldas",
                    "MG",
                    210,
                    "APTO. 7",
                    clienteId
                )
            };
        }

        public async Task<bool> CriarEndereco(Endereco endereco)
        {
            return true;
        }

        public async Task<bool> ExcluirEndereco(Guid id)
        {
            return true;
        }
    }
}
