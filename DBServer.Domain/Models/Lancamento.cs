using System;
using System.Collections.Generic;
using System.Text;

namespace DBServer.Domain.Models
{
    public class Lancamentos
    {
        public Lancamentos()
        {

            Debitos = new List<Lancamento>();
            Creditos = new List<Lancamento>();
        }
        /// <summary>
        /// Creditos que ocorreram na conta corrente
        /// </summary>
        public List<Lancamento> Creditos { get; set; }

        /// <summary>
        /// Debitos que ocorreram na conta corrente
        /// </summary>
        public List<Lancamento> Debitos { get; set; }
    }

    public class Lancamento
    {
        public int LancamentoId { get; set; }
        public string ContaOrigem { get; set; }
        public string ContaDestino { get; set; }
        public DateTime DataRealizacao { get; set; }
        public decimal Valor { get; set; }
        public string TX { get; set; }
    }
}