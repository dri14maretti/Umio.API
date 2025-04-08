using Newtonsoft.Json;
using Umio.API.Application.Contratos;
using Umio.API.Entities.Entidades;
using Umio.API.ViaCepService.Models;

namespace Umio.API.ViaCepService.Services
{
    public class CepService : ICepService
    {
        private const string _url = "https://viacep.com.br/ws/{0}/json/";

        public async Task<Endereco> BuscarEnderecoPorCep(string cep)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = string.Format(_url, cep);
                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var enderecoViaCep = JsonConvert.DeserializeObject<ViaCepEnderecoModel>(jsonResponse);
                    return Endereco.CriarEnderecoSemNumero(cep, enderecoViaCep.logradouro, enderecoViaCep.bairro, enderecoViaCep.localidade, enderecoViaCep.uf);
                }

                throw new Exception();
            }
        }
    }
}
