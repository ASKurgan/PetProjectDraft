using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.ValueObjects
{
    // Данный класс - удобная замена enum-ам
    public class Place : ValueObject
    {
        public static readonly Place InHospital = new(nameof(InHospital).ToUpper());
        public static readonly Place AtHome = new(nameof(AtHome).ToUpper());

        private static readonly Place[] _all = [InHospital, AtHome];

        public string Value { get; }

        private Place(string value)
        {
            Value = value;
        }

        public static Result<Place> Create(string input)
        {
            if (input.IsEmpty() || input.Length > Constraints.SHORT_TITLE_LENGTH)
                return Errors.General.InvalidLength();

            var place = input.Trim().ToUpper();

            if (_all.Any(p => p.Value == place) == false)
            {
                return Errors.General.ValueIsInvalid(nameof(place));
            }

            return new Place(place);
        }

        // Здесь в IEnumerable в списке будет одно свойство : Value 
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;  // возвращает значение коллекции текущего итератора
        }
    }
}
