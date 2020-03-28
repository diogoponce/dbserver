using System;
using System.Collections.Generic;
using System.Text;

namespace DBServer.Domain.Exceptions
{
    public class LancamentoException : Exception
    {
        public LancamentoException()
        {

        }

        public LancamentoException(string message) : base(message)
        {
        }

        public LancamentoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
