using System;
using System.Collections.Generic;
using System.Text;

namespace DBServer.Domain.Exceptions
{
    public class ContaCorrenteException : Exception
    {
        public ContaCorrenteException()
        {

        }

        public ContaCorrenteException(string message) : base(message)
        {
        }

        public ContaCorrenteException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
