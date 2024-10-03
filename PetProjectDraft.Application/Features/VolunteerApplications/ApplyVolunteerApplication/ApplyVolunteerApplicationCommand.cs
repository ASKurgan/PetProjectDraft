using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.VolunteerApplications.ApplyVolunteerApplication
{
    public record ApplyVolunteerApplicationCommand(
      string FirstName,
      string LastName,
      string? Patronymic,
      string Email,
      string Description,
      int YearsExperience,
      int? NumberOfPetsFoundHome,
      bool FromShelter);
}
