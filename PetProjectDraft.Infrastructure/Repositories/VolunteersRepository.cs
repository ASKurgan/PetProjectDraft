using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PetProjectDraft.Application.Features.Volunteers;
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
    public class VolunteersRepository : IVolunteersRepository
    {
        private readonly PetProjectWriteDbContext _dbContext;

        public VolunteersRepository(PetProjectWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Volunteer volunteer, CancellationToken ct)
        {
            await _dbContext.Volunteers.AddAsync(volunteer, ct);
        }

        public async Task<IReadOnlyList<Volunteer>> GetAllWithPhotos(CancellationToken ct)
        {
            var volunteers = await _dbContext.Volunteers
            .Include(v => v.Photos)
                .ToListAsync(cancellationToken: ct);

            return volunteers;
        }

        public async Task<Result<Volunteer>> GetById(Guid id, CancellationToken ct)
        {
            var volunteer = await _dbContext.Volunteers
                .Include(v => v.Pets)
                .Include(v => v.Photos)
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken: ct);

            if (volunteer is null)
                return Errors.General.NotFound(id);

            return volunteer;
        }
    }
}
