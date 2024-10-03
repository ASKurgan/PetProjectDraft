using PetProjectDraft.Application.DataAccess;
using PetProjectDraft.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Infrastructure.Providers
{
    public class Transaction : ITransaction
    {
        private readonly PetProjectWriteDbContext _dbContext;

        public Transaction(PetProjectWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
