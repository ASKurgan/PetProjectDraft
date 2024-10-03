using Microsoft.AspNetCore.Identity.Data;
using PetProjectDraft.Application.DataAccess;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.Entities;
using PetProjectDraft.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.Users.Register
{
    public class RegisterHandler
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITransaction _transaction;

        public RegisterHandler(IUsersRepository usersRepository, ITransaction transaction)
        {
            _usersRepository = usersRepository;
            _transaction = transaction;
        }

        public async Task<Result> Handle(RegisterCommand command, CancellationToken ct)
        {
            var email = Email.Create(command.Email).Value;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

            var user = User.CreateRegularUser(email, passwordHash);

            if (user.IsFailure)
                return user.Error;

            await _usersRepository.Add(user.Value, ct);

            await _transaction.SaveChangesAsync(ct);
            
            return Result.Success();
        }
    }
}
