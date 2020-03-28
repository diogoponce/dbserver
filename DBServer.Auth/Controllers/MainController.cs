using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DBServer.Application.ContaCorrente;
using DBServer.Domain.Parameters;
using DBServer.Domain.ResultSet;
using DBServer.Infra.Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DBServer.Auth.Controllers
{
    /// <summary>
    /// Serviço de autenticação da conta corrente
    /// </summary>
    [Route("")]
    public class MainController : Controller
    {
        private readonly IConfiguration Configuration;
        private ContaCorrenteApp ContaCorrente { get; set; }

        /// <summary>
        /// DI e Inicializador da conta corrente
        /// </summary>
        /// <param name="configuration"></param>
        public MainController(IConfiguration configuration)
        {
            Configuration = configuration;
            ContaCorrente = ContaCorrenteApp.Create(configuration);
        }

        /// <summary>
        /// Realiza a autenticação na conta corrente do cliente
        /// </summary>
        /// <param name="contaCorrente"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult<SecurityTokenResult>> Post([FromBody] ContaCorrenteParam contaCorrente)
        {
            try
            {                
                //Realiza o login
                await ContaCorrente.LoginAsync(contaCorrente).ConfigureAwait(true);
                //Obtem o token de autenticação
                var token = GetAuthToken(contaCorrente);
                return Json(token);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtém o token de autenticação
        /// </summary>
        /// <param name="contaCorrente"></param>
        /// <returns></returns>
        private SecurityTokenResult GetAuthToken(ContaCorrenteParam contaCorrente)
        {
            try
            {

                var SecurityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(Configuration.GetSection("AppSecretKey").Value));
                var TokenHandler = new JwtSecurityTokenHandler();

                ///Criamos uma identidade
                ClaimsIdentity claimsIdentity = new ClaimsIdentity();
                var encText = TwofishEngine.Instance.Encrypt(new CriptoParameter
                {
                    Salt = Encoding.Default.GetBytes(Configuration.GetSection("AppSecretKey").Value),
                    Data = JsonConvert.SerializeObject(contaCorrente),
                    Password = Configuration.GetSection("AppSecretKey").Value
                });
                ///Claim de acesso
                Collection<Claim> claims = new Collection<Claim>
                {
                    new Claim(ClaimTypes.Authentication, encText )
                };

                ///Cria os claims do inscritos
                claimsIdentity.AddClaims(claims);

                ///Configura os Token
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = Configuration.GetSection("Issuer").Value,
                    Subject = claimsIdentity,
                    SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256, SecurityAlgorithms.Sha256Digest)
                };

                ///Gera o Token e retorna a string
                SecurityToken token = TokenHandler.CreateToken(tokenDescriptor);
                var tokenString = TokenHandler.WriteToken(token);

                ///Retorna
                return new SecurityTokenResult
                {
                    Create = DateTime.UtcNow,
                    Token = tokenString,
                    Expires = DateTime.UtcNow.AddHours(1),
                };
            }
            catch
            {
                throw;
            }
        }
    }
}