using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.Entities
{
    // VolunteerApplication в переводе означает "заявка на волонтёрство" 
    public class VolunteerApplication : Entity
    {
        private VolunteerApplication() {}

        public VolunteerApplication(
            FullName fullName,
            Email email,
            string description,
            int yearsExperience,
            int? numberOfPetsFoundHome,
            bool fromShelter)
        {
            FullName = fullName;
            Email = email;
            Description = description;
            YearsExperience = yearsExperience;
            NumberOfPetsFoundHome = numberOfPetsFoundHome;
            FromShelter = fromShelter;
            Status = ApplicationStatus.Consideration;
        }

        public ApplicationStatus Status { get; private set; } = null!;
        public FullName FullName { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public int YearsExperience { get; private set; }
        public int? NumberOfPetsFoundHome { get; private set; }
        public bool FromShelter { get; private set; }

        public Result Approve()
        {
            if (Status == ApplicationStatus.Approved)
                return Errors.VolunteersApplications.AlreadyApproved();

            Status = ApplicationStatus.Approved;
            return Result.Success();
        }
    }

    // одобрена (Approved)
    // на рассмотрении (Consideration)
    // отклонена (Denied)

}
