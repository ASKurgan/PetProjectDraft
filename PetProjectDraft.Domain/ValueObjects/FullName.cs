﻿using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.ValueObjects
{
    public class FullName : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string? Patronymic { get; private set; }

        private FullName(string firstName, string lastName, string? patronymic)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
        }

        public static Result<FullName> Create(string firstName, string lastName, string? patronymic)
        {
            firstName = firstName.Trim();
            lastName = lastName.Trim();
            patronymic = patronymic?.Trim();

            if (firstName.IsEmpty())
                return Errors.General.ValueIsRequired(nameof(firstName));

            if (lastName.IsEmpty())
                return Errors.General.ValueIsRequired(nameof(lastName));

            if (firstName.Length is < 1 or > Constraints.SHORT_TITLE_LENGTH)
                return Errors.General.InvalidLength();

            if (lastName.Length is < 1 or > Constraints.SHORT_TITLE_LENGTH)
                return Errors.General.InvalidLength();

            if (patronymic?.Length is < 1 or > Constraints.SHORT_TITLE_LENGTH)
                return Errors.General.InvalidLength();

            return new FullName(firstName, lastName, patronymic);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
