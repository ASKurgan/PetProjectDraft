using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.Entities
{
    public class User : Entity
    {
        private User() { }

        private User(Email email, string passwordHash, Role role)
        {
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }

        public Email Email { get; private set; }
        public string PasswordHash { get; private set; }
        public Role Role { get; private set; }
        public long? TelegramId { get; private set; }

        public void AddTelegram(long telegramId)
        {
            TelegramId = telegramId;
        }


        public static Result<User> CreateVolunteer(Email email, string passwordHash)
        {
            return new User(email, passwordHash, Role.Volunteer);
        }

        public static Result<User> CreateAdmin(Email email, string passwordHash)
        {
            return new User(email, passwordHash, Role.Admin);
        }

        public static Result<User> CreateRegularUser(Email email, string passwordHash)
        {
            return new User(email, passwordHash, Role.RegularUser);
        }
    }
}
