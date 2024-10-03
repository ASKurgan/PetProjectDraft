using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; }

        public Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string input)
        {
            input = input.Trim();

            if (input.Length is < 1 or > Constraints.SHORT_TITLE_LENGTH)
                return Errors.General.InvalidLength("email");

            if (Regex.IsMatch(input, "^(.+)@(.+)$") == false)
                return Errors.General.ValueIsInvalid("email");

            return new Email(input);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
