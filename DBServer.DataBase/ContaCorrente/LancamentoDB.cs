using DBServer.DataBase.Core;
using DBServer.Domain.Models;
using DBServer.Domain.Parameters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace DBServer.DataBase.ContaCorrente
{
    public class LancamentoDB
    {
        readonly IConfiguration Configuration;
        public LancamentoDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static LancamentoDB Create(IConfiguration configuration)
        {
            try
            {
                return new LancamentoDB(configuration);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Lancamento>> ObterLancamentos(ContaCorrenteParam contaCorrente)
        {
            try
            {
                if (contaCorrente is null)
                    throw new ArgumentNullException(nameof(contaCorrente));

                var param = new { Conta = $"{contaCorrente.Numero}-{contaCorrente.Digito}" };
                return await Repository<Lancamento>
                             .Instance
                             .SetConnectionString(Configuration.GetConnectionString("DefaultConnection").ToString(CultureInfo.CurrentCulture))
                             .QueryAsync(@"SELECT LancamentoId,
                                               ContaOrigem,
                                               ContaDestino,
                                               DataRealizacao,
                                               Valor,
                                               TX 
                                           FROM dbo.Lacamentos 
                                           WHERE ContaOrigem = @Conta OR ContaDestino = @Conta", param)
                             .ConfigureAwait(true) ?? new List<Lancamento>();
            }
            catch
            {
                throw;
            }
        }

    }
}
