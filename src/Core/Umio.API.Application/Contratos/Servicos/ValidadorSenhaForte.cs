using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Umio.API.Application.Contratos.Servicos
{
    public class ValidadorSenhaForte
    {
        public static bool SenhaForte(string senha)
        {
            return !string.IsNullOrWhiteSpace(senha) &&
            senha.Length >= 6 &&
            senha.Any(char.IsUpper) &&
            senha.Any(char.IsLower) &&
            senha.Any(char.IsDigit) &&
            senha.Any(c => !char.IsLetterOrDigit(c));
        }
    }
}