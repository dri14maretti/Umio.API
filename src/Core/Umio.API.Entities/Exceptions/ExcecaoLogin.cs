namespace Umio.API.Entities.Exceptions
{
    public class ExcecaoLogin : Exception
    {
        public ExcecaoLogin() : base("O email ou senha informados não estão corretos")
        {
        }
    }
}
