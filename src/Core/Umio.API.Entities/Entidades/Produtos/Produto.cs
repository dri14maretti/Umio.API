namespace Umio.API.Entities.Entidades.Produtos
{
    public class Produto
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public string Descricao { get; private set; }
        public string Comentarios { get; private set; }
        public string Imagem { get; private set; }
        public string Categoria { get; private set; }
        public bool HabilitarAdicionais { get; private set; }
        public bool HabilitarMolhos { get; private set; }
        public bool HabilitarAcompanhamentos { get; private set; }

        public Produto(Guid id, string nome, decimal preco, string descricao, string comentarios, string imagem)
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
