﻿using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.Entities
{
    public class PetPhoto : Photo
    {
        private PetPhoto(string path, bool isMain)
            : base(path, isMain)
        {
        }

        public static Result<PetPhoto> Create(
            string contentType,
            long length)
        {
            if (contentType != JPG && contentType != JPEG && contentType != PNG)
                return Errors.Volunteers.FileTypeInvalid(contentType);

            if (length > 1000000)
                return Errors.Volunteers.FileLengthInvalid(length);

            var path = Guid.NewGuid() + contentType;

            return new PetPhoto(path, false);
        }
    }
}
