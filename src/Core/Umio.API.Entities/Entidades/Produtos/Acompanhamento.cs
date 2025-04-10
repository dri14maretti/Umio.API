namespace Umio.API.Entities.Entidades.Produtos
{
    public class Acompanhamento : Produto
    {
        public Acompanhamento(Guid id, string nome, decimal preco, string descricao, string comentarios, string imagem) : base(id, nome, preco, descricao, comentarios, imagem)
        {
        }
    }
}
