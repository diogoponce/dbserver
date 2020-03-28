using DBServer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBServer.Domain.Models
{
    public class ContaCorrentePadrao : IContaCorrente
    {
        public ContaCorrentePadrao()
        {
            Lancamentos = new Lancamentos();
        }
        /// <summary>
        /// Data de abertura da conta
        /// </summary>
        public DateTime DataAbertura { get ; set ; }
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
        /// Saldo da conta
        /// </summary>
        public decimal Saldo { get; set; }

        /// <summary>
        /// Lançamentos da conta corrente
        /// </summary>
        public Lancamentos Lancamentos { get; set; }

        /// <summary>
        /// Cliente dono da conta
        /// </summary>
        public Cliente Cliente { get; set; }

    }

    public class ContaCorrenteMaster : IContaCorrente
    {
        /// <summary>
        /// Data de abertura da conta
        /// </summary>
        public DateTime DataAbertura { get; set; }
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
        /// Saldo da conta
        /// </summary>
        public decimal Saldo { get; set; }

        /// <summary>
        /// Lançamentos da conta corrente
        /// </summary>
        public Lancamentos Lancamentos { get; set; }

        /// Cliente dono da conta
        /// </summary>
        public Cliente Cliente { get; set; }

        /// <summary>
        /// Limite de cheque especial.
        /// Apenas para contas Master Exclusivo 
        /// </summary>
        public decimal Limite { get; set; }
        
    }
}