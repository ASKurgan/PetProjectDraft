using Microsoft.AspNetCore.Http;
using PetProjectDraft.Application.Dtos;
using PetProjectDraft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.Volunteers.PublishPet
{
    public record PublishPetCommand(
    Guid VolunteerId,
    string Nickname,
    string Description,
    DateTimeOffset BirthDate,
    string Breed,
    string Color,
    string City,
    string Street,
    string Building,
    string Index,
    string Place,
    bool Castration,
    string PeopleAttitude,
    string AnimalAttitude,
    bool OnlyOneInFamily,
    string Health,
    int Height,
    float Weight,
    string ContactPhoneNumber,
    string VolunteerPhoneNumber,
    bool OnTreatment,
    IEnumerable<VaccinationDto> Vaccinations,
    IEnumerable<PetPhoto> Photos);
}
