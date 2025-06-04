using System.ComponentModel.DataAnnotations.Schema;

namespace Umio.API.Entities.Entidades
{
    [Table("categoriaproduto")]
    public class CategoriaProduto
    {
        public CategoriaProduto(int id, string categoria)
        {
            Id = id;
            Categoria = categoria;
        }
        [Column("id")]
        public int Id { get; private set; }
        [Column("categoria")]
        public string Categoria { get; private set; }

    }
}
