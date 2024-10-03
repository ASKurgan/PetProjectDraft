using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public const string RUSSIAN_PHONE_REGEX = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";

        public string Number { get; }

        private PhoneNumber(string number)
        {
            Number = number;
        }

        public static Result<PhoneNumber> Create(string input)
        {
            input = input.Trim();

            if (input.Length is < Constraints.MINIMUM_TITLE_LENGTH or < Constraints.MINIMUM_TITLE_LENGTH)
                return Errors.General.InvalidLength(nameof(PhoneNumber));

            if (Regex.IsMatch(input, RUSSIAN_PHONE_REGEX) == false)
                return Errors.General.ValueIsInvalid(nameof(PhoneNumber));

            return new PhoneNumber(input);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
