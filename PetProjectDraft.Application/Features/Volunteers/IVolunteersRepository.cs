using Microsoft.AspNetCore.Http.HttpResults;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.Volunteers
{
    public interface IVolunteersRepository
    {
        Task Add(Volunteer volunteer, CancellationToken ct);
        Task<Result<Volunteer>> GetById(Guid id, CancellationToken ct);
        Task<IReadOnlyList<Volunteer>> GetAllWithPhotos(CancellationToken ct);
    }
}
