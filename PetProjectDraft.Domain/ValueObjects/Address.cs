using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public const int INDEX_TITLE_LENGTH = 6;

        private Address(string city, string street, string building, string index)
        {
            City = city;
            Building = building;
            Street = street;
            Index = index;
        }

        public string City { get;}
        public string Building { get; }
        public string Street { get; }
        public string Index { get; }


        public static Result<Address> Create (string city, string street, string building, string index)
        {
            city = city.Trim();
            building = building.Trim();
            index = index.Trim();

            if (city.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.SHORT_TITLE_LENGTH)
                return Errors.General.InvalidLength(nameof(city));

            if (street.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.MEDIUM_TITLE_LENGTH)
                return Errors.General.InvalidLength(nameof(street));

            if (building.IsEmpty() || building.Length > Constraints.SHORT_TITLE_LENGTH)
                return Errors.General.InvalidLength(nameof(building));

            if (index.Length != INDEX_TITLE_LENGTH)
                return Errors.General.InvalidLength(nameof(index));

            return new Address(city,street,building,index);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return City;
            yield return Building;
            yield return Street;
        }
    }
}
