using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PetProjectDraft.Domain.Common.Permissions;

namespace PetProjectDraft.Domain.Entities
{
    public class Volunteer : Entity
    {
        public const int PHOTO_COUNT_LIMIT = 5;

        private Volunteer()
        {}

        public Volunteer(
            Guid id,
            FullName fullName,
            string description,
            int yearsExperience,
            int? numberOfPetsFoundHome,
            string? donationInfo,
            bool fromShelter,
            IEnumerable<SocialMedia> socialMedias) : base(id)
        {
            FullName = fullName;
            Description = description;
            YearsExperience = yearsExperience;
            NumberOfPetsFoundHome = numberOfPetsFoundHome;
            DonationInfo = donationInfo;
            FromShelter = fromShelter;
            _socialMedias = socialMedias.ToList();
        }

        public FullName FullName { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public int YearsExperience { get; private set; }
        public int? NumberOfPetsFoundHome { get; private set; }
        public string? DonationInfo { get; private set; }
        public bool FromShelter { get; private set; }

        public IReadOnlyList<VolunteerPhoto> Photos => _photos;
        private readonly List<VolunteerPhoto> _photos = [];

        public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;
        private readonly List<SocialMedia> _socialMedias = [];

        public IReadOnlyList<Pet> Pets => _pets;
        private readonly List<Pet> _pets = [];

        public void PublishPet(Pet pet)
        {
            _pets.Add(pet);
        }

        public Result<Guid> AddPhoto(VolunteerPhoto volunteerPhoto)
        {
            if (_photos.Count >= PHOTO_COUNT_LIMIT)
            {
                return Errors.Volunteers.PhotoCountLimit();
            }

            _photos.Add(volunteerPhoto);
            return volunteerPhoto.Id;
            // return Result.Success();
        }

        public Result<Guid> DeletePhoto(string path)
        {
            var photo = _photos.FirstOrDefault(p => p.Path.Contains(path));
            if (photo is null)
                return Errors.General.NotFound();
            var photoId = photo.Id;
            _photos.Remove(photo);
            return photoId;
            // return Result.Success();
        }

        public static Result<Volunteer> Create(
            Guid userId,
            FullName fullname,
            string description,
            int yearsExperience,
            int? numberOfPetsFoundHome,
            string? donationInfo,
            bool fromShelter,
            IEnumerable<SocialMedia> socialMedias)
        {
            if (userId == Guid.Empty)
                return Errors.General.ValueIsInvalid(nameof(userId));

            if (description.IsEmpty() || description.Length > Constraints.LONG_TITLE_LENGTH)
                return Errors.General.InvalidLength(nameof(description));

            if (yearsExperience is < 0 or > 100)
                return Errors.General.ValueIsInvalid(nameof(yearsExperience));

            if (numberOfPetsFoundHome < 0)
                return Errors.General.ValueIsInvalid(nameof(numberOfPetsFoundHome));

            if (donationInfo?.Length > Constraints.LONG_TITLE_LENGTH)
                return Errors.General.InvalidLength(nameof(donationInfo));

            return new Volunteer(
                userId,
                fullname,
                description,
                yearsExperience,
                numberOfPetsFoundHome,
                donationInfo,
                fromShelter,
                socialMedias);
        }
    }

}
