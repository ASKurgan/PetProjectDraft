using Microsoft.EntityFrameworkCore;
using PetProjectDraft.Application.Features.VolunteerApplications;
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
    public class VolunteerApplicationsRepository : IVolunteerApplicationsRepository
    {
        private readonly PetProjectWriteDbContext _dbContext;

        public VolunteerApplicationsRepository(PetProjectWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<VolunteerApplication>> GetById(Guid id, CancellationToken ct)
        {
            var application = await _dbContext.VolunteersApplications
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken: ct);

            if (application is null)
                return Errors.General.NotFound(id);

            return application;
        }

        public async Task Add(VolunteerApplication application, CancellationToken ct)
        {
            await _dbContext.VolunteersApplications.AddAsync(application, ct);
        }
    }
}
