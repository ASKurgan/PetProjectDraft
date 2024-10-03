using Microsoft.Extensions.Logging;
using PetProjectDraft.Application.DataAccess;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.Entities;
using PetProjectDraft.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.VolunteerApplications.ApplyVolunteerApplication
{
    // Apply Volunteer Application переводится как "подать заявку на волонтёра" . Handler - обработчик
    public class ApplyVolunteerApplicationHandler
    {
        private readonly ITransaction _transaction;
        private readonly IVolunteerApplicationsRepository _volunteerApplicationsRepository;
        private readonly ILogger<ApplyVolunteerApplicationHandler> _logger;

        public ApplyVolunteerApplicationHandler(
            ITransaction transaction,
            IVolunteerApplicationsRepository volunteerApplicationsRepository,
            ILogger<ApplyVolunteerApplicationHandler> logger)
        {
            _transaction = transaction;
            _volunteerApplicationsRepository = volunteerApplicationsRepository;
            _logger = logger;
        }

        public async Task<Result<Guid>> Handle(ApplyVolunteerApplicationCommand command, CancellationToken ct)
        {
            var fullName = FullName.Create(
                command.FirstName, command.LastName, command.Patronymic).Value;

            // Черновой пример проверки
            //var fullNameResult = FullName.Create(
            //    command.FirstName, command.LastName, command.Patronymic);


            //if (fullNameResult.IsFailure)
            //{
            //    return new Result<Guid>(Guid.Empty, false, Error.Deserialize(fullNameResult.Error.ToString()));

            //}


            // ущё один черновой вариант проверки
            //var fullNameResult = FullName.Create(
            //    request.FirstName, request.LastName, request.Patronymic);


            //if (fullNameResult.IsFailure)
            //{
            //   // return new Result<Guid>(Guid.Empty, false, Error.Deserialize(fullNameResult.Error.ToString()));
            //   return fullNameResult.Error;
            //}

            var email = Email.Create(command.Email).Value;

            var application = new VolunteerApplication(
                fullName,
                email,
                command.Description,
                command.YearsExperience,
                command.NumberOfPetsFoundHome,
                command.FromShelter);

            await _volunteerApplicationsRepository.Add(application, ct);
            await _transaction.SaveChangesAsync(ct);

            _logger.LogInformation("Volunteer application has been created {id}", application.Id);

            return application.Id;
        }
    }

}
