using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Umio.API.Application.Contratos.Servicos
{
    public class CryptgrafaSenha
    {
        public static string CryptoSenha(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] senhaBytes = Encoding.UTF8.GetBytes(senha);
                byte[] hashBytes = sha256.ComputeHash(senhaBytes);

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}