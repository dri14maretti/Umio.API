using System.ComponentModel.DataAnnotations.Schema;

namespace Umio.API.Entities.Entidades.Produtos
{
    [Table("produto")]
    public class Produto
    {
        [Column("id")]
        public int Id { get; private set; }
        [Column("nome")]
        public string Nome { get; private set; }
        [Column("preco")]
        public decimal Preco { get; private set; }
        [Column("descricao")]
        public string Descricao { get; private set; }
        [Column("imagem")]
        public string? Imagem { get; private set; }
        [Column("categoriaid")]
        public int CategoriaId { get; private set; }
        [Column("ativo")]
        public bool Ativo { get; private set; } = true;

        private Produto(int id, string nome, decimal preco, string descricao, string imagem, int categoriaId, bool ativo)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            Imagem = imagem;
            CategoriaId = categoriaId;
            Ativo = ativo;
        }
    }
}
