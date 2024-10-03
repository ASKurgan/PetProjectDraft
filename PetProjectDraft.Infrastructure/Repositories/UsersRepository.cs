using Microsoft.EntityFrameworkCore;
using PetProjectDraft.Application.Features.Users;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.Entities;
using PetProjectDraft.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly PetProjectWriteDbContext _dbContext;

        public UsersRepository(PetProjectWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(User user, CancellationToken ct)
        {
            await _dbContext.AddAsync(user, ct);
        }

        public async Task<Result<User>> GetById(Guid id, CancellationToken ct)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken: ct);

            if (user is null)
                return Errors.General.NotFound();

            return user;
        }

        public async Task<Result<User>> GetByEmail(string email, CancellationToken ct)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email.Value == email, cancellationToken: ct);

            if (user is null)
                return Errors.General.NotFound();

            return user;
        }
    }
}
