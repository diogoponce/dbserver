using SHA3.Net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DBServer.Infra
{
    public static class Helpers
    {
        /// <summary>
        /// Faz o HASH SHA-3 da senha
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static string HashPassword(this string Password)
        {
            StringBuilder x = new StringBuilder();

            foreach (var item in Sha3.Sha3256().ComputeHash(Encoding.UTF8.GetBytes(Password)))
            {
                x.Append(item.ToString("x2", CultureInfo.CurrentCulture));
            }

            return x.ToString();
        }

        public static long ToUnixTimeStamp(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        }

    }
}
