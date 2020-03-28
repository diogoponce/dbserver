using DBServer.Domain.Models;
using System;
using System.Collections.Generic;

namespace DBServer.Domain.Interfaces
{
    /// <summary>
    /// Representa uma conta corrente no sistema
    /// </summary>
    public interface IContaCorrente
    {

        /// <summary>
        /// Data de abertura da conta
        /// </summary>
        DateTime DataAbertura { get; set; }
        /// <summary>
        /// Numero da conta
        /// </summary>
        int Numero { get; set; }
        /// <summary>
        /// Digito da conta
        /// </summary>
        int Digito { get; set; }
        /// <summary>
        /// Senha de acesso
        /// </summary>
        string Senha { get; set; }
        /// <summary>
        /// Saldo da conta
        /// </summary>
        decimal Saldo { get; set; }

        /// <summary>
        /// Lançamentos da conta corrente
        /// </summary>
        Lancamentos Lancamentos { get; set; }

        /// <summary>
        /// Cliente dono da conta
        /// </summary>
        Cliente Cliente { get; set; }

    }
}
