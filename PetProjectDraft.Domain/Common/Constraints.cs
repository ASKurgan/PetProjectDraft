using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.Common
{
    public readonly struct Constraints
    {
        public const int SHORT_TITLE_LENGTH = 150;
        public const int MEDIUM_TITLE_LENGTH = 1000;
        public const int LONG_TITLE_LENGTH = 5000;

        public const int MINIMUM_TITLE_LENGTH = 1;
        public const int MINIMUM_SYMBOLS_FOR_PASSWORD = 6;
    }
}
