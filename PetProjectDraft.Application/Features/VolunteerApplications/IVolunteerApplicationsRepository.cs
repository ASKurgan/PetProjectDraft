using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.VolunteerApplications
{
    public interface IVolunteerApplicationsRepository
    {
        Task<Result<VolunteerApplication>> GetById(Guid id, CancellationToken ct);
        Task Add(VolunteerApplication application, CancellationToken ct);
    }
}
