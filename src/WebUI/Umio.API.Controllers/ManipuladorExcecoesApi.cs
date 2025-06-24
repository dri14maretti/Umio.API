using System.Net;
using System.Text.Json;
using Umio.API.Controllers.Models;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Controllers
{
    public class ManipuladorExcecoesApi : IMiddleware
    {
        private readonly ILogger<ManipuladorExcecoesApi> _logger;

        public ManipuladorExcecoesApi(ILogger<ManipuladorExcecoesApi> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ExcecaoPropriedadeInvalida exPropriedadeInvalida)
            {
                GerarHistoricoErros(exPropriedadeInvalida);
                var json = GerarRetorno(context, exPropriedadeInvalida);
                await GerarRespostaContexto(context, json, HttpStatusCode.BadRequest);
            }
            catch (ExcecaoLogin exLogin)
            {
                GerarHistoricoErros(exLogin);
                var json = GerarRetorno(context, exLogin);
                await GerarRespostaContexto(context, json, HttpStatusCode.BadRequest);
            }
            catch (ExcecaoParametroIncorreto exParametroIncorreto)
            {
                GerarHistoricoErros(exParametroIncorreto);
                var json = GerarRetorno(context, exParametroIncorreto);
                await GerarRespostaContexto(context, json, HttpStatusCode.BadRequest);
            }
            catch (ExcecaoElementoNaoEncontrado exElemento)
            {
                GerarHistoricoErros(exElemento);
                var json = GerarRetorno(context, exElemento);
                await GerarRespostaContexto(context, json, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                GerarHistoricoErros(ex);
                var json = GerarRetorno(context, ex);
                await GerarRespostaContexto(context, json);
            }
        }

        private string GerarRetorno(HttpContext context, Exception ex)
        {
            var retorno = ApiRetorno<string>.Falha(ex.Message);
            return JsonSerializer.Serialize(retorno);
        }

        private async Task GerarRespostaContexto(HttpContext context, string json, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        {
            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }

        private void GerarHistoricoErros(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
