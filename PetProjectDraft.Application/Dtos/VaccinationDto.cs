using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Dtos
{
    public record VaccinationDto(string Name, DateTimeOffset? Applied);
}
