namespace PetProjectDraft.Api.Requests.VolunteerApplications.ApplyVolunteerApplication
{
    public record ApplyVolunteerApplicationRequest(
    string FirstName,
    string LastName,
    string? Patronymic,
    string Email,
    string Description,
    int YearsExperience,
    int? NumberOfPetsFoundHome,
    bool FromShelter);
}
