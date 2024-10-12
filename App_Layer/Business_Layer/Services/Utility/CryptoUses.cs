using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Utility
{
    public class CryptoUses
    {
        public static string GenerateRandomCode(int length)
        {
            var randomBytes = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            return BitConverter.ToString(randomBytes).Replace("-", "").Substring(0, length);
        }
    }
}
