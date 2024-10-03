using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.ValueObjects
{
    public class Weight : ValueObject
    {
        public float Kilograms { get; set; }

        private Weight(float kilograms)
        {
            Kilograms = kilograms;
        }

        public static Result<Weight> Create(float kilograms)
        {
            if (kilograms <= 0)
                return Errors.General.ValueIsInvalid(nameof(Weight));

            return new Weight(kilograms);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Kilograms; // поля, или свойства, по которым будет происходить сравнение в нашем
                                    // переопределённом Equals()
        }
    }
}
