using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using PetProjectDraft.Application.Interfaces.Providers;
using PetProjectDraft.Domain.Entities;
using PetProjectDraft.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetProjectDraft.Application.DataAccess;
using PetProjectDraft.Domain.Common;

namespace PetProjectDraft.Application.Features.Volunteers.PublishPet
{
    public class PublishPetHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ITransaction _transaction;
        private readonly IMinioProvider _minioProvider;
        public PublishPetHandler(
            IVolunteersRepository volunteersRepository,
            ITransaction transaction,
            IMinioProvider minioProvider)
        {
            _volunteersRepository = volunteersRepository;
            _transaction = transaction; 
            _minioProvider = minioProvider;
        }

        public async Task<Result<Guid>> Handle(PublishPetCommand command, CancellationToken ct)
        {
            var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, ct);
            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var volunteer = volunteerResult.Value;

            var address = Address.Create(command.City, command.Street, command.Building, command.Index).Value;
            var place = Place.Create(command.Place).Value;
            var weight = Weight.Create(command.Weight).Value;
            var contactPhoneNumber = PhoneNumber.Create(command.ContactPhoneNumber).Value;
            var volunteerPhoneNumber = PhoneNumber.Create(command.VolunteerPhoneNumber).Value;

           

            

            var vaccinations = command.Vaccinations.Select(v => Vaccination.Create(v.Name, v.Applied).Value);

            var pet = Pet.Create(
                command.Nickname,
                command.Description,
                command.BirthDate,
                command.Breed,
                command.Color,
                address,
                place,
                command.Castration,
                command.PeopleAttitude,
                command.AnimalAttitude,
                command.OnlyOneInFamily,
                command.Health,
                command.Height,
                weight,
                contactPhoneNumber,
                volunteerPhoneNumber,
                command.OnTreatment,
                vaccinations,
                command.Photos);

            if (pet.IsFailure)
                return pet.Error;

            volunteer.PublishPet(pet.Value);

            

            await _transaction.SaveChangesAsync(ct);
            return volunteer.Id;
        }

     

        // Draft Methods
        //public async Task<Domain.Common.Result<bool>> SetPhotoInMinIo(Stream stream, string photoPath, CancellationToken ct)
        //{
        //    var objectName = await _minioProvider
        //           .UploadPhoto(stream, photoPath, ct);

        //    if (objectName.IsFailure)
        //        return objectName.Error;

        //    return objectName.IsSucces;


        //}

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
