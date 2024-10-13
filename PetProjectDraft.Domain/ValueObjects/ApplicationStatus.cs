using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.ValueObjects
{
    public class ApplicationStatus : ValueObject
    {
        public static readonly ApplicationStatus Denied = new(nameof(Denied).ToUpper());
        public static readonly ApplicationStatus Consideration = new(nameof(Consideration).ToUpper());
        public static readonly ApplicationStatus Approved = new(nameof(Approved).ToUpper());

        private static readonly ApplicationStatus[] _all = [Denied, Consideration, Approved];

        public string Status { get; private set; }

        private ApplicationStatus(string status)
        {
            Status = status;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Status;
        }
    }
}
