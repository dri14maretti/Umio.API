using System.Text.RegularExpressions;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Entities.Entidades
{
    public class Endereco
    {
        public Guid? Id { get; private set; }
        public string Cep { get; private set; }
        public string Rua { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string UF { get; private set; }
        public int Numero { get; private set; }
        public string? Complemento { get; private set; }
        public Guid UsuarioId { get; private set; }

        private Endereco(string cep, string rua, string bairro, string cidade, string uf, int numero, string complemento, Guid usuarioId)
        {
            Cep = cep;
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
            Numero = numero;
            UsuarioId = usuarioId;
        }

        private Endereco(string cep, string rua, string bairro, string cidade, string uf)
        {
            Cep = cep;
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
        }
        public static Endereco CriarEnderecoSemNumero(string cep, string rua, string bairro, string cidade, string uf)
        {
            return new Endereco(cep, rua, bairro, cidade, uf);
        }

        public static Endereco CriarNovoEndereco(string cep, string rua, string bairro, string cidade, string uf, int numero, string complemento, Guid usuarioId)
        {
            if (string.IsNullOrWhiteSpace(cep) || !Regex.IsMatch(cep, @"^\d{8}$"))
                throw new ExcecaoPropriedadeInvalida("CEP inválido.", nameof(cep));
            if (string.IsNullOrWhiteSpace(rua))
                throw new ExcecaoPropriedadeInvalida("Rua inválida.", nameof(rua));
            if (string.IsNullOrWhiteSpace(bairro))
                throw new ExcecaoPropriedadeInvalida("Bairro inválido.", nameof(bairro));
            if (string.IsNullOrWhiteSpace(cidade))
                throw new ExcecaoPropriedadeInvalida("Cidade inválida.", nameof(cidade));
            if (string.IsNullOrWhiteSpace(uf) || uf.Length != 2)
                throw new ExcecaoPropriedadeInvalida("UF inválido.", nameof(uf));
            if (numero <= 0)
                throw new ExcecaoPropriedadeInvalida("Número inválido.", nameof(numero));
            if (complemento != null && complemento.Length > 50)
                throw new ExcecaoPropriedadeInvalida("Complemento inválido.", nameof(complemento));
            if (usuarioId == Guid.Empty)
                throw new ExcecaoPropriedadeInvalida("Usuário inválido.", nameof(usuarioId));

            return new Endereco(cep, rua, bairro, cidade, uf, numero, complemento, usuarioId);
        }
    }
}
