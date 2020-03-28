using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DBServer.Application.ContaCorrente;
using DBServer.Domain.Models;
using DBServer.Domain.Parameters;
using DBServer.Infra;
using DBServer.Infra.Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DBServer.Balance.Controllers
{
    [Route("")]
    [Authorize]
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
        /// Retorna o saldo da conta corrente
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<decimal>> Post()
        {
            try
            {
                return Ok(await ObterSaldo().ConfigureAwait(true));
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
        /// Obtém o saldo da conta corrente
        /// </summary>
        /// <returns></returns>
        private async Task<decimal> ObterSaldo()
        {
            var data = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Authentication).FirstOrDefault()?.Value;

            if (data is null)
                throw new UnauthorizedAccessException();

            var criptoParam = new CriptoParameter
            {
                Data = data,
                Password = Configuration.GetSection("AppSecretKey").Value,
                Salt = Encoding.Default.GetBytes(Configuration.GetSection("AppSecretKey").Value)
            };

            var contaCorrente = JsonConvert.DeserializeObject<ContaCorrentePadrao>(TwofishEngine.Instance.Decrypt(criptoParam));
            var obter = await ContaCorrente.ObterContaCorrenteAsync(new ContaCorrenteParam
            {
                Numero = contaCorrente.Numero,
                Digito = contaCorrente.Digito,
                Senha = contaCorrente.Senha
            }).ConfigureAwait(true);
            return obter.Saldo;
        }
    }
}