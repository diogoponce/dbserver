using DBServer.DataBase.ContaCorrente;
using DBServer.Domain.Interfaces;
using DBServer.Domain.Models;
using DBServer.Domain.Parameters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBServer.Application.ContaCorrente
{
    public class ContaCorrenteApp
    {
        private ContaCorrenteDB ContaCorrente { get; set; }
        private LancamentoDB Lancamentos { get; set; }

        public ContaCorrenteApp(IConfiguration configuration)
        {
            Lancamentos = LancamentoDB.Create(configuration);
            ContaCorrente = ContaCorrenteDB.Create(configuration);
        }

        public static ContaCorrenteApp Create(IConfiguration configuration)
        {
            try
            {
                return new ContaCorrenteApp(configuration);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IContaCorrente> LoginAsync(ContaCorrenteParam contaCorrente)
        {
            var login = await this.ContaCorrente
                                  .ObterContaCorrenteAsync(contaCorrente)
                                  .ConfigureAwait(true);

            if (login is null)
                throw new UnauthorizedAccessException();

            return login;
        }

        public async Task<IContaCorrente> ObterContaCorrenteAsync(ContaCorrenteParam contaCorrente)
        {
            try
            {

                if (contaCorrente is null)
                    throw new ArgumentNullException(nameof(contaCorrente));

                var conta = $"{contaCorrente.Numero}-{contaCorrente.Digito}";
                var result = await this.ContaCorrente
                                 .ObterContaCorrenteAsync(contaCorrente)
                                 .ConfigureAwait(true);

                var lancamentos = await Lancamentos
                                        .ObterLancamentos(contaCorrente)
                                        .ConfigureAwait(true);

                if (lancamentos?.Count > 0)
                {
                    result.Lancamentos.Debitos = lancamentos
                                       .Where(c => c.ContaOrigem.Equals(conta, StringComparison.InvariantCulture))
                                       .ToList() ?? new List<Lancamento>();

                    result.Lancamentos.Creditos = lancamentos
                                       .Where(c => c.ContaDestino.Equals(conta, StringComparison.InvariantCulture))
                                       .ToList() ?? new List<Lancamento>();
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> NovaContaCorrenteAsync(ContaCorrenteParam contaCorrente)
        {
            return await this.ContaCorrente
                             .NovaContaCorrenteAsync(contaCorrente)
                             .ConfigureAwait(true);
        }

        public async Task<bool> TransferirFundosAsync(TransferParam transferParam)
        {
            return await ContaCorrente
                         .TransferirFundosAsync(transferParam)
                         .ConfigureAwait(true);
        }

    }
}
