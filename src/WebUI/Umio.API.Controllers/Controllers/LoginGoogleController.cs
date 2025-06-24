using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.TokenService.Models;
using Umio.API.Entities.Entidades;
using Umio.API.Entities.Entidades.Enums;
using Umio.API.Application.Contratos.Servicos;

namespace Umio.API.Controllers.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ConfiguracoesJwt _jwtSettings;
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public AuthController(
            IConfiguration configuration,
            IClienteRepository clienteRepository,
            IUsuarioRepository usuarioRepository,
            ITokenService tokenService)
        {
            _jwtSettings = new ConfiguracoesJwt();
            configuration.GetSection("JwtSettings").Bind(_jwtSettings);

            _clienteRepository = clienteRepository;
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleCallback", "Auth", null, Request.Scheme)
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded)
                return Unauthorized("Falha ao autenticar com Google.");

            var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var name = result.Principal.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(email))
                return BadRequest("Não foi possível recuperar o e-mail do Google.");

            var cliente = await _clienteRepository.BuscarPorEmail(email);

            if (cliente == null)
            {
                //todo: pedir ao front para criar um modal requerindo o numero de telefone do cliente.
                var telefoneFake = "00000000000";

                cliente = Cliente.CriarNovoCliente(name, email, telefoneFake);
                await _clienteRepository.CriarCliente(cliente);
            }

            var usuario = await _usuarioRepository.BuscarPorClienteIdProvedorId(cliente.Id, Provedor.Google);

            if (usuario == null)
            {
                usuario = Usuario.CriarUsuarioOAuth(cliente.Id, Provedor.Google);
                await _usuarioRepository.CriarUsuario(usuario);
            }

            var token = _tokenService.GerarToken(cliente.Id);
            
            return Ok(new
            {
                Token = token,
                Email = email,
                Nome = name
            });
        }
    }
}