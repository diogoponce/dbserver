using System;
using System.Collections.Generic;
using System.Text;

namespace DBServer.Domain.Parameters
{
    public class TransferParam
    {
        public int OrigemNumero { get; set; }
        public int OrigemDigito { get; set; }
        public string OrigemSenha { get; set; }
        public int DestinoNumero { get; set; }
        public int DestinoDigito { get; set; }
        public decimal DestinoValor { get; set; }
    }
}

