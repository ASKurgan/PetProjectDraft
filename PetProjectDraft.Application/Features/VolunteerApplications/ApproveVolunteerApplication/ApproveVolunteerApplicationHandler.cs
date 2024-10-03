using Microsoft.Extensions.Logging;
using PetProjectDraft.Application.DataAccess;
using PetProjectDraft.Application.Features.Users;
using PetProjectDraft.Application.Features.Volunteers;
using PetProjectDraft.Application.Interfaces.MessageBus;
using PetProjectDraft.Application.Messages;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.VolunteerApplications.ApproveVolunteerApplication
{
    public class ApproveVolunteerApplicationHandler
    {
        private readonly IVolunteerApplicationsRepository _volunteerApplicationsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ITransaction _transaction;
        private readonly ILogger<ApproveVolunteerApplicationHandler> _logger;
        private readonly IMessageBus _messageBus;

        public ApproveVolunteerApplicationHandler(
            IVolunteerApplicationsRepository volunteerApplicationsRepository,
            IUsersRepository usersRepository,
            IVolunteersRepository volunteersRepository,
            ITransaction transaction,
            ILogger<ApproveVolunteerApplicationHandler> logger,
            IMessageBus messageBus)
        {
            _volunteerApplicationsRepository = volunteerApplicationsRepository;
            _usersRepository = usersRepository;
            _volunteersRepository = volunteersRepository;
            _transaction = transaction;
            _logger = logger;
            _messageBus = messageBus;
        }

        public async Task<Result> Handle(ApproveVolunteerApplicationCommand command, CancellationToken ct)
        {
            var volunteerApplicationResult = await _volunteerApplicationsRepository.GetById(command.Id, ct);
            if (volunteerApplicationResult.IsFailure)
                return volunteerApplicationResult.Error;

            var volunteerApplication = volunteerApplicationResult.Value;

            var approvedResult = volunteerApplication.Approve();
            if (approvedResult.IsFailure)
                return approvedResult.Error;

            var password = RandomPassword.Generate();
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = User.CreateVolunteer(volunteerApplication.Email, passwordHash);
            if (user.IsFailure)
                return user.Error;

            await _usersRepository.Add(user.Value, ct);

            var volunteer = Volunteer.Create(
                user.Value.Id,
                volunteerApplication.FullName,
                volunteerApplication.Description,
                volunteerApplication.YearsExperience,
                volunteerApplication.NumberOfPetsFoundHome,
                null,
                volunteerApplication.FromShelter,
                []);

            if (volunteer.IsFailure)
                return volunteer.Error;

            await _volunteersRepository.Add(volunteer.Value, ct);

            await _transaction.SaveChangesAsync(ct);

            _logger.LogInformation(
                "Volunteer application has been successfully approved and volunteer has been created with id: {id}",
                volunteer.Value.Id);

            var emailNotification = new EmailNotification(
                $"Вы успешно зарегистрировались в Pet Family." +
                $" Логин: {volunteerApplication.Email.Value}, Пароль: {password}",
                volunteerApplication.Email);

            await _messageBus.PublishAsync(emailNotification, ct);

            return Result.Success();
        }
    }
}
