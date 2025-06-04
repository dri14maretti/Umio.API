using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umio.API.Entities.Entidades;
using Umio.API.Entities.Entidades.Enums;

namespace Umio.API.TestData.Entidades
{
    public static class DadosUsuario
    {
        public static string Senha => "Senha@123";
        public static Guid ClienteId => Guid.NewGuid();
        public static Provedor Provedor => Provedor.Google;

        public static Usuario UsuarioValido => Usuario.CriarNovoUsuario(Senha, ClienteId, Provedor);

        public static Usuario UsuarioComSenhaForte => Usuario.CriarNovoUsuario("SenhaForte@456", ClienteId, Provedor);

        public static Usuario UsuarioComSenhaFraca => Usuario.CriarNovoUsuario("12345", ClienteId, Provedor);
    }
}
