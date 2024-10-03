using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.Users.Register
{
    public record RegisterCommand(string Email, string Password);
}
