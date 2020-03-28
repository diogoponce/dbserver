using System;
using System.Collections.Generic;
using System.Text;

namespace DBServer.Domain.Models
{
    public class Cliente
    {
        public long ClienteId { get; set; }
        public long CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NomeCompleto { get; set; }
    }
}
