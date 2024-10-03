using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.Users.Login
{
    public record LoginResponse(string Token, string Role);
}
