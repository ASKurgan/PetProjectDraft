using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.Users
{
    public interface IUsersRepository
    {
        Task<Result<User>> GetByEmail(string email, CancellationToken ct);
        Task<Result<User>> GetById(Guid id, CancellationToken ct);
        public Task Add(User user, CancellationToken ct);
    }
}
