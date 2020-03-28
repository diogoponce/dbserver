using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DBServer.Application.ContaCorrente;
using DBServer.Domain.Exceptions;
using DBServer.Domain.Models;
using DBServer.Domain.Parameters;
using DBServer.Infra;
using DBServer.Infra.Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DBServer.Transactions.Controllers
{
    /// <summary>
    /// Serviço que realiza transferencia 
    /// entre contas corrente
    /// </summary>
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
            ContaCorrente = ContaCorrenteApp.Create(Configuration);
        }

        /// <summary>
        /// Realiza a transferencia entre contas
        /// </summary>
        /// <param name="contaCorrente"></param>
        /// <returns></returns>
        public async Task<ActionResult<bool>> Post([FromBody]ContaCorrenteParam transferOrder)
        {
            try
            {
                if (transferOrder is null)
                    throw new ArgumentNullException(nameof(transferOrder));

                var transferParam = GetTransferParam(transferOrder);
                var transferirFundos = await ContaCorrente.TransferirFundosAsync(transferParam).ConfigureAwait(true);
                return Ok(transferirFundos);
            }
            catch(ContaCorrenteException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Cria os parametros para a transação
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private TransferParam GetTransferParam(ContaCorrenteParam transaction)
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

            var cliente = JsonConvert.DeserializeObject<ContaCorrentePadrao>(TwofishEngine.Instance.Decrypt(criptoParam));

            return new TransferParam
            {
                OrigemNumero = cliente.Numero,
                OrigemDigito = cliente.Digito,
                OrigemSenha = cliente.Senha.HashPassword(),
                DestinoNumero = transaction.Numero,
                DestinoDigito = transaction.Digito,
                DestinoValor = transaction.Valor
            };
        }
    }
}