using DBServer.Application.ContaCorrente;
using DBServer.Domain.Parameters;
using DBServer.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Threading.Tasks;
using Xunit;


namespace DBServer.XTest
{
    public class UnitTest1
    {
        readonly IConfiguration Configuration;
        ContaCorrenteApp ContaCorrenteApp { get; set; }

        public UnitTest1()
        {
            Configuration = new ConfigurationBuilder()
                                .AddJsonFile(@"C:\data\dbserver\appsettings\default.test.json")
                                .Build();
            ContaCorrenteApp = ContaCorrenteApp.Create(Configuration);
        }

        [Fact]

        public async Task ObterContaCorrenteAsync()
        {
            var data = new ContaCorrenteParam
            {
                Numero = 895623,
                Digito = 1,
                Senha = "235689"
            };
            
            var result = await ContaCorrenteApp.ObterContaCorrenteAsync(data);
            Assert.True(result.Numero > 0);
        }

        [Fact]
        public async Task NovaContaCorrente1Async()
        {
            var data = new ContaCorrenteParam
            {
                ClienteId = 1,
                Numero = 895623,
                Digito = 1,
                Senha = "235689"
            };
            var result = await ContaCorrenteApp.Create(Configuration).NovaContaCorrenteAsync(data);
            Assert.True(result);
        }

        [Fact]
        public async Task NovaContaCorrente2Async()
        {
            var data = new ContaCorrenteParam
            {
                ClienteId = 3,
                Numero = 784512,
                Digito = 1,
                Senha = "215487"
            };            
            var result = await ContaCorrenteApp.NovaContaCorrenteAsync(data);
            Assert.True(result);
        }

        [Fact]
        public async Task TransferirFundosAsync()
        {
            var data = new TransferParam
            {
                OrigemNumero = 895623,
                OrigemDigito = 1,
                OrigemSenha = "235689",
                DestinoNumero = 784512,
                DestinoDigito = 1,
                DestinoValor = 10
            };

            var result = await ContaCorrenteApp.TransferirFundosAsync(data);
            Assert.True(result);
        }

    }
}
