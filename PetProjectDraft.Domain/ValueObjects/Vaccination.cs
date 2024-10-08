﻿using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.ValueObjects
{
    public class Vaccination : ValueObject
    {
        private Vaccination() { }

        private Vaccination(string name, DateTimeOffset? applied)
        {
            Name = name;
            Applied = applied;
        }

        public string Name { get; private set; } = null!;

        public DateTimeOffset? Applied { get; private set; }


        public static Result<Vaccination> Create(
            string name,
            DateTimeOffset? applied)
        {
            name = name.Trim();

            if (name.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.SHORT_TITLE_LENGTH)
                return Errors.General.InvalidLength(nameof(Vaccination));

            if (applied > DateTimeOffset.UtcNow)
                return Errors.General.ValueIsInvalid(nameof(Vaccination));

            return new Vaccination(name, applied);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
