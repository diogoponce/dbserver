using System;
using System.Collections.Generic;
using System.Text;

namespace DBServer.Domain.Enums
{
    [Flags]
    public enum Moedas
    {
        Dolar,
        Euro,
        Real
    }

    [Flags]
    public enum TipoConta
    {
        Padrao,
        Master
    }
}
