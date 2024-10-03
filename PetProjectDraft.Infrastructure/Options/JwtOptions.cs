using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Infrastructure.Options
{
    public class JwtOptions
    {
        public const string Jwt = nameof(Jwt);

        public string SecretKey { get; init; } = string.Empty;
        public int Expires { get; init; }
    }
}
