using System;
using System.Collections.Generic;
using System.Text;

namespace DBServer.Domain.Parameters
{
    public class ContaCorrenteParam
    {
        /// <summary>
        /// Id do cliente dono da conta
        /// </summary>
        public int ClienteId { get; set; }
        /// <summary>
        /// Numero da conta
        /// </summary>
        public int Numero { get; set; }
        /// <summary>
        /// Digito da conta
        /// </summary>
        public int Digito { get; set; }
        /// <summary>
        /// Senha de acesso
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Valor da transação
        /// </summary>
        public decimal Valor { get; set; }
    }
}
