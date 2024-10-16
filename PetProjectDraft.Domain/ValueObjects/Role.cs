﻿using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.ValueObjects
{
    public class Role : ValueObject
    {
        public static readonly Role Admin = new(
            "ADMIN",
            [
                Common.Permissions.VolunteerApplications.Update,

            Common.Permissions.Pets.Read,
            Common.Permissions.Pets.Delete,

            Common.Permissions.Volunteers.Create,
            Common.Permissions.Volunteers.Delete,
            Common.Permissions.Volunteers.Read,
            ]);

        public static readonly Role Volunteer = new(
            "VOLUNTEER",
            [
                Common.Permissions.Pets.Read,
            Common.Permissions.Pets.Create,
            Common.Permissions.Pets.Update,
            Common.Permissions.Pets.Delete,

            Common.Permissions.Volunteers.Read,
            ]);

        public static readonly Role RegularUser = new(
            "REGULARUSER",
            [
              Common.Permissions.Pets.Read,
              Common.Permissions.Volunteers.Read,
            ]);

        private Role(string name, string[] permissions)
        {
            Name = name;
            Permissions = permissions;
        }

        public string Name { get; private set; }
        public string[] Permissions { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
