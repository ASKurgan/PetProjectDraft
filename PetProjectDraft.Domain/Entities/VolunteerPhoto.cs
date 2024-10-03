using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.Entities
{
    public class VolunteerPhoto : Photo
    {
        public VolunteerPhoto(string path, bool isMain)
            : base(path, isMain)
        {
        }

        public static new Result<VolunteerPhoto> CreateAndActivate(
                                                                string path,
                                                                string contentType,
                                                                long length,
                                                                bool isMain
                                                                     )
        {
            if (contentType != JPG && contentType != JPEG && contentType != PNG)
                return Errors.Volunteers.FileTypeInvalid(contentType);
            if (length > 10000)
                return Errors.Volunteers.FileLengthInvalid(length);
            return new VolunteerPhoto(path, isMain);
        }
    }
}
