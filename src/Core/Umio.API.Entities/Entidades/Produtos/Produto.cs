namespace Umio.API.Entities.Entidades.Produtos
{
    public abstract class Produto
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public decimal Preco { get; protected set; }
        public string Descricao { get; protected set; }
        public string Comentarios { get; protected set; }
        public string Imagem { get; protected set; }

        protected Produto(Guid id, string nome, decimal preco, string descricao, string comentarios, string imagem)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            Comentarios = comentarios;
            Imagem = imagem;
        }

    }
}
