using Microsoft.AspNetCore.Http.HttpResults;
using PetProjectDraft.Api.Models;
using PetProjectDraft.Api.Requests.PublishPet;
using PetProjectDraft.Application.Features.Volunteers.PublishPet;
using PetProjectDraft.Application.Interfaces.Providers;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.Entities;

namespace PetProjectDraft.Api.Mappers.MapperPublishPet
{
    public class MapFromRequestToCommand
    {
        private readonly IMinioProvider _minioProvider;

        public MapFromRequestToCommand(IMinioProvider minioProvider)
        {
            _minioProvider = minioProvider;
        }
        public async Task<Result<PublishPetCommand>> GetPublishPetCommand(PublishPetRequest request,
                                                                                     CancellationToken ct)
        {
            var photoFiles = GetPhotoFiles(request.Files);
            if (photoFiles.IsFailure)
                return photoFiles.Error;

            foreach (var photoFile in photoFiles.Value)
            {
                await using var stream = photoFile.File.OpenReadStream();

                var objectName = await _minioProvider
                    .UploadPhoto(stream, photoFile.PetPhoto.Path, ct);

                if (objectName.IsFailure)
                    return objectName.Error;
            }

            var photos = photoFiles.Value.Select(p => p.PetPhoto);
            //foreach (var photo in photos)
            //{

            //}
            var command = new PublishPetCommand(request.VolunteerId,
                                                request.Nickname,
                                                request.Description,
                                                request.BirthDate,
                                                request.Breed,
                                                request.Color,
                                                request.City,
                                                request.Street,
                                                request.Building,
                                                request.Index,
                                                request.Place,
                                                request.Castration,
                                                request.PeopleAttitude,
                                                request.AnimalAttitude,
                                                request.OnlyOneInFamily,
                                                request.Health,
                                                request.Height,
                                                request.Weight,
                                                request.ContactPhoneNumber,
                                                request.VolunteerPhoneNumber,
            request.OnTreatment,
                                                request.Vaccinations,
                                                photos);


            return command;
        }

        private Result<List<PhotoFile>> GetPhotoFiles(IFormFileCollection fileCollection)
        {
            List<PhotoFile> photos = [];
            foreach (var file in fileCollection)
            {
                var contentType = Path.GetExtension(file.FileName);

                var photo = PetPhoto.Create(contentType, file.Length);
                if (photo.IsFailure)
                    return photo.Error;

                photos.Add(new(photo.Value, file));
            }

            return photos;
        }

        //public static Result<MapRequestCommand,Error> Get()
        //{



        //    var instance = new MapRequestCommand();
        //}
    }
    //public class MapFromRequestToCommand
    //{
    //}
}
