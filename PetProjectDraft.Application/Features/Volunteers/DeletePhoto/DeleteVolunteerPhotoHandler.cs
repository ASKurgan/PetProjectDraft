using Microsoft.AspNetCore.Http.HttpResults;
using PetProjectDraft.Application.DataAccess;
using PetProjectDraft.Application.Interfaces.Providers;
using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.Volunteers.DeletePhoto
{
    public class DeleteVolunteerPhotoHandler
    {
        private readonly IMinioProvider _minioProvider;
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ITransaction _transaction;

        public DeleteVolunteerPhotoHandler(
            IMinioProvider minioProvider,
            IVolunteersRepository volunteersRepository,
            ITransaction transaction)
        {
            _minioProvider = minioProvider;
            _volunteersRepository = volunteersRepository;
            _transaction = transaction;
        }

        public async Task<Result<bool>> Handle(
            DeleteVolunteerPhotoCommand command,
            CancellationToken ct)
        {
            var volunteer = await _volunteersRepository.GetById(command.VolunteerId, ct);
            if (volunteer.IsFailure)
                return volunteer.Error;

            var isRemove = await _minioProvider.RemovePhoto(command.Path, ct);
            if (isRemove.IsFailure)
                return isRemove.Error;

            var isDelete = volunteer.Value.DeletePhoto(command.Path);
            if (isDelete.IsFailure)
                return isDelete.Error;

            await _transaction.SaveChangesAsync(ct);

            return true;
        }
    }
}
