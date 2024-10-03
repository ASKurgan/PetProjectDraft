using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.Common
{
    public static class RandomPassword
    {
        public static string Generate(int length = 12)
        {
            const string passwordOptions = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var password = new string(Enumerable.Repeat(passwordOptions, length)
                .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)])
                .ToArray());

            return password;
        }
    }
}
