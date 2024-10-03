namespace PetProjectDraft.Api.Requests.UploadPhoto
{
    public record UploadVolunteerPhotoRequest(Guid VolunteerId, IFormFile File, bool IsMain);
}
