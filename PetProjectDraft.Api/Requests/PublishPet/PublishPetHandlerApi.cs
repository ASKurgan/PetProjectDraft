using Microsoft.AspNetCore.Http.HttpResults;
using PetProjectDraft.Api.Models;
using PetProjectDraft.Application.DataAccess;
using PetProjectDraft.Application.Features.Volunteers;
using PetProjectDraft.Application.Interfaces.Providers;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.Entities;
using PetProjectDraft.Domain.ValueObjects;

namespace PetProjectDraft.Api.Requests.PublishPet
{
    public class PublishPetHandlerApi
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ITransaction _transaction;
        private readonly IMinioProvider _minioProvider;
        public PublishPetHandlerApi(
            IVolunteersRepository volunteersRepository,
            ITransaction transaction,
            IMinioProvider minioProvider)
        {
            _volunteersRepository = volunteersRepository;
            _transaction = transaction;
            _minioProvider = minioProvider;
        }

        public async Task<Result<Guid>> Handle(PublishPetRequest request, CancellationToken ct)
        {
            var volunteer = await _volunteersRepository.GetById(request.VolunteerId, ct);
            if (volunteer.IsFailure)
                return volunteer.Error;

            var address = Address.Create(request.City, request.Street, request.Building, request.Index).Value;
            var place = Place.Create(request.Place).Value;
            var weight = Weight.Create(request.Weight).Value;
            var contactPhoneNumber = PhoneNumber.Create(request.ContactPhoneNumber).Value;
            var volunteerPhoneNumber = PhoneNumber.Create(request.VolunteerPhoneNumber).Value;

            var photoFiles = GetPhotoFiles(request.Files);
            if (photoFiles.IsFailure)
                return photoFiles.Error;

            var photos = photoFiles.Value.Select(p => p.PetPhoto);

            var vaccinations = request.Vaccinations.Select(v => Vaccination.Create(v.Name, v.Applied).Value);

            var pet = Pet.Create(
                request.Nickname,
                request.Description,
                request.BirthDate,
                request.Breed,
                request.Color,
                address,
                place,
                request.Castration,
                request.PeopleAttitude,
                request.AnimalAttitude,
                request.OnlyOneInFamily,
                request.Health,
                request.Height,
                weight,
                contactPhoneNumber,
                volunteerPhoneNumber,
                request.OnTreatment,
                vaccinations,
                photos);

            if (pet.IsFailure)
                return pet.Error;

            volunteer.Value.PublishPet(pet.Value);

            foreach (var photoFile in photoFiles.Value)
            {
                await using var stream = photoFile.File.OpenReadStream();

                var objectName = await _minioProvider
                .UploadPhoto(stream, photoFile.PetPhoto.Path, ct);

                if (objectName.IsFailure)
                    return objectName.Error;
            }

            await _transaction.SaveChangesAsync(ct);
            return volunteer.Value.Id;
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

        // Draft Methods
        public async Task<Result<bool>> SetPhotoInMinIo(Stream stream, string photoPath, CancellationToken ct)
        {
            var objectName = await _minioProvider
                   .UploadPhoto(stream, photoPath, ct);

            if (objectName.IsFailure)
                return objectName.Error;

            return objectName.IsSucces;


        }

        //private async Task<Domain.Common.Result<bool>> SavePhotoInMinIo(Stream stream, string photoPath, CancellationToken ct)
        //{
        //    var objectName = await _minioProvider
        //           .UploadPhoto(stream, photoPath, ct);

        //    if (objectName.IsFailure)
        //        return objectName.Error;

        //    return objectName.IsSuccess;
        //}
    }
}
