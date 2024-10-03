using PetProjectDraft.Domain.Entities;

namespace PetProjectDraft.Api.Models
{
    public record PhotoFile(PetPhoto PetPhoto, IFormFile File);
}
