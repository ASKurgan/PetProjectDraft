using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.Volunteers.DeletePhoto
{
    public record DeleteVolunteerPhotoCommand(Guid VolunteerId, string Path);
}
