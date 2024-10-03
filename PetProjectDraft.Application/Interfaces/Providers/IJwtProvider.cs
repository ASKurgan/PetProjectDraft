using Microsoft.AspNetCore.Http.HttpResults;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Interfaces.Providers
{
    public interface IJwtProvider
    {
        Result<string> Generate(User user);
    }
}
