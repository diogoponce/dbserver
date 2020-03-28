using DBServer.DataBase.Core;
using DBServer.Domain.Exceptions;
using DBServer.Domain.Interfaces;
using DBServer.Domain.Models;
using DBServer.Domain.Parameters;
using DBServer.Infra;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace DBServer.DataBase.ContaCorrente
{
    public class ContaCorrenteDB
    {
        readonly IConfiguration Configuration;
        public ContaCorrenteDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static ContaCorrenteDB Create(IConfiguration configuration)
        {
            try
            {
                return new ContaCorrenteDB(configuration);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IContaCorrente> ObterContaCorrenteAsync(ContaCorrenteParam contaCorrente)
        {
            try
            {
                if (contaCorrente is null)
                    throw new ArgumentNullException(nameof(contaCorrente));

                var param = new
                {
                    contaCorrente.Numero,
                    contaCorrente.Digito,
                    Senha = contaCorrente.Senha.HashPassword()
                };

                return await Repository<ContaCorrentePadrao>
                             .Instance
                             .SetConnectionString(Configuration.GetConnectionString("DefaultConnection").ToString(CultureInfo.CurrentCulture))
                             .QueryFirstOrDefaultAsync(@"SELECT ContaCorrenteId,
                                                                ClienteId,
                                                                DataAbertura,
                                                                Numero,
                                                                Digito,
                                                                Senha,
                                                                Saldo,
                                                                TipoConta
                                                        FROM dbo.ContaCorrente
                                                        WHERE Numero = @Numero
                                                                AND Digito = @Digito
                                                                AND Senha = @Senha", param)
                             .ConfigureAwait(true);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> NovaContaCorrenteAsync(ContaCorrenteParam contaCorrente)
        {
            try
            {
                if (contaCorrente is null)
                    throw new ArgumentNullException(nameof(contaCorrente));

                var param = new
                {
                    contaCorrente.ClienteId,
                    contaCorrente.Numero,
                    contaCorrente.Digito,
                    Senha = contaCorrente.Senha.HashPassword(),
                };
                var resultSet = await Repository<SqlResutSet>
                                     .Instance
                                     .SetConnectionString(Configuration.GetConnectionString("DefaultConnection").ToString(CultureInfo.CurrentCulture))
                                     .QueryFirstOrDefaultAsync("usp_insContaCorrente", param, System.Data.CommandType.StoredProcedure)
                                     .ConfigureAwait(true);
                //
                if (resultSet.ID > 0)
                    return true;

                throw new ContaCorrenteException(resultSet.Message);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> TransferirFundosAsync(TransferParam param)
        {
            try
            {
                var resultSet = await Repository<SqlResutSet>
                                     .Instance
                                     .SetConnectionString(Configuration.GetConnectionString("DefaultConnection").ToString(CultureInfo.CurrentCulture))
                                     .QueryFirstOrDefaultAsync("usp_cmdTranferTo", param, System.Data.CommandType.StoredProcedure)
                                     .ConfigureAwait(true);

                //Verifica se deu tudo certo
                if (resultSet.ID == 1)
                    return true;

                throw new ContaCorrenteException(resultSet.Message);

            }
            catch
            {
                throw;
            }
        }

    }
}
