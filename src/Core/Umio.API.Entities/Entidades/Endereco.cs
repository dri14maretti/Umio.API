using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Entities.Entidades
{
    [Table("endereco")]
    public class Endereco
    {
        [Column("id")]
        public Guid? Id { get; private set; }
        [Column("cep")]
        public string Cep { get; private set; }
        [Column("rua")]
        public string Rua { get; private set; }
        [Column("bairro")]
        public string Bairro { get; private set; }
        [Column("cidade")]
        public string Cidade { get; private set; }
        [Column("uf")]
        public string UF { get; private set; }
        [Column("numero")]
        public int Numero { get; private set; }
        [Column("complemento")]
        public string? Complemento { get; private set; }
        [Column("ativo")]
        public bool Ativo { get; private set; }
        [Column("clienteid")]
        public Guid ClienteId { get; private set; }

        private Endereco(string cep, string rua, string bairro, string cidade, string uf, int numero, string complemento, bool ativo, Guid usuarioId)
        {
            Id = Guid.NewGuid();
            Cep = cep;
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
            Numero = numero;
            ClienteId = usuarioId;
            Ativo = true;
            Complemento = complemento;
        }

        private Endereco(string cep, string rua, string bairro, string cidade, string uf)
        {
            Cep = cep;
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
        }

        public Endereco(Guid? id, string cep, string rua, string bairro, string cidade, string uF, int numero, string? complemento, bool ativo, Guid clienteId)
        {
            Id = id;
            Cep = cep;
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            UF = uF;
            Numero = numero;
            Complemento = complemento;
            Ativo = ativo;
            ClienteId = clienteId;
        }

        public static Endereco CriarEnderecoSemNumero(string cep, string rua, string bairro, string cidade, string uf)
        {
            return new Endereco(cep, rua, bairro, cidade, uf);
        }

        public static Endereco CriarNovoEndereco(string cep, string rua, string bairro, string cidade, string uf, int numero, Guid clienteId, string? complemento = null)
        {
            if (string.IsNullOrWhiteSpace(cep) || !Regex.IsMatch(cep, @"^\d{8}$"))
                throw new ExcecaoPropriedadeInvalida(nameof(cep), "CEP inválido.");
            if (string.IsNullOrWhiteSpace(rua))
                throw new ExcecaoPropriedadeInvalida(nameof(rua), "Rua inválida.");
            if (string.IsNullOrWhiteSpace(bairro))
                throw new ExcecaoPropriedadeInvalida(nameof(bairro), "Bairro inválido.");
            if (string.IsNullOrWhiteSpace(cidade))
                throw new ExcecaoPropriedadeInvalida(nameof(cidade), "Cidade inválida.");
            if (string.IsNullOrWhiteSpace(uf) || uf.Length != 2)
                throw new ExcecaoPropriedadeInvalida(nameof(uf), "UF inválido.");
            if (numero <= 0)
                throw new ExcecaoPropriedadeInvalida(nameof(numero), "Número inválido.");
            if (clienteId == Guid.Empty)
                throw new ExcecaoPropriedadeInvalida(nameof(clienteId), "Usuário inválido.");

            return new Endereco(cep, rua, bairro, cidade, uf, numero, complemento, true, clienteId);
        }

        public void DesativarEndereco()
        {
            Ativo = false;
        }
    }
}
