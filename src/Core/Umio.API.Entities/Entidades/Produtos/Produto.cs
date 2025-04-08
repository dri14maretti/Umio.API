namespace Umio.API.Entities.Entidades.Produtos
{
    public abstract class Produto
    {
        protected Produto(Guid id, string nome, double preco, string descricao, string comentarios, string imagem)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            Comentarios = comentarios;
            Imagem = imagem;
        }

        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public double Preco { get; protected set; }
        public string Descricao { get; protected set; }
        public string Comentarios { get; protected set; }
        public string Imagem { get; protected set; }
        
    }
}
