using Umio.API.Entities.ObjetosDeValor;

namespace Umio.API.Entities.Entidades.Produtos
{
    public class Lanche : Produto
    {
        private Lanche(Guid id, string nome, decimal preco, string descricao, string comentarios, string imagem) : base(id, nome, preco, descricao, comentarios, imagem)
        {
            Adicionais = new List<Adicional>();
        }

        public List<Adicional> Adicionais { get; private set; } = [];
        public List<Molho> Molhos { get; private set; } = [];

        public void ColocarAdicionais(List<Adicional> adicionais)
        {
            Adicionais = adicionais;
        }
    }
}
