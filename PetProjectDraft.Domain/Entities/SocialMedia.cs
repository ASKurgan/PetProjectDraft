using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.Entities
{
    public class SocialMedia : ValueObject
    {
        private SocialMedia(string link, Social social)
        {
            Link = link;
            Social = social;
        }

        public string Link { get; private set; }
        public Social Social { get; private set; }

        public static Result<SocialMedia> Create(string link, Social social)
        {
            link = link.Trim();

            if (link.IsEmpty() || link.Length > Constraints.LONG_TITLE_LENGTH)
                return Errors.General.InvalidLength();

            return new SocialMedia(
                link,
                social);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Link;
            yield return Social;
        }
    }
}
