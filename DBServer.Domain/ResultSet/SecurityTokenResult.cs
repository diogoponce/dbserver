using System;
using System.Collections.Generic;
using System.Text;

namespace DBServer.Domain.ResultSet
{
    public class SecurityTokenResult
    {

        public SecurityTokenResult()
        {
            Expires = DateTime.MinValue;
        }

        public DateTime Create { get; set; }
        public DateTime Expires { get; set; }
        public string Token { get; set; }
    }
}
