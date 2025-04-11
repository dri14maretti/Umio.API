using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Umio.API.Controllers.Models
{
    public class ApiRetorno<T> : ActionResult, IStatusCodeActionResult
    {
        public T Dados { get; private set; }
        public string Mensagem { get; private set; }
        public bool Ok { get; private set; }
        public int? StatusCode { get; private set; }

        public static ApiRetorno<T> Falha(string mensagemErro)
        {
            return new ApiRetorno<T> { Ok = false, Mensagem = mensagemErro };
        }
        public static ApiRetorno<T> Falha(T dados)
        {
            return new ApiRetorno<T> { Ok = false, Dados = dados };
        }

        public static ApiRetorno<T> Sucesso(T dados)
        {
            return new ApiRetorno<T> { Ok = true, Dados = dados };
        }

        public static ApiRetorno<T> Sucesso()
        {
            return new ApiRetorno<T> { Ok = true };
        }
    }
}
