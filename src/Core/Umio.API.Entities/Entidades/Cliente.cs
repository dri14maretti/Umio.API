namespace Umio.API.Entities.Entidades
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Nome { get; private set; }
        public int Pontos { get; private set; }
        public string Foto { get; private set; }
    }
}
