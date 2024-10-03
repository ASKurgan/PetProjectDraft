using Microsoft.AspNetCore.Http;
using PetProjectDraft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.Volunteers.UploadPhoto
{
    public record UploadVolunteerPhotoCommand(Guid VolunteerId, VolunteerPhoto Photo, bool IsMain);
}
