﻿using FluentValidation;
using PetProjectDraft.Application.ComonValidators;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.Volunteers.PublishPet
{
    public class PublishPetCommandValidator // :  AbstractValidator<PublishPetRequest>
    {
        //public PublishPetRequestValidator()
        //{
        //    RuleFor(x => x.ContactPhoneNumber).MustBeValueObject(PhoneNumber.Create);
        //    RuleFor(x => x.VolunteerPhoneNumber).MustBeValueObject(PhoneNumber.Create);
        //    RuleFor(x => x.Weight).MustBeValueObject(Weight.Create);
        //    RuleFor(x => x.Place).MustBeValueObject(Place.Create);
        //    RuleFor(x => new { x.City, x.Street, x.Building, x.Index })
        //        .MustBeValueObject(x => Address.Create(x.City, x.Street, x.Building, x.Index));

        //    RuleFor(x => x.Nickname)
        //        .NotEmptyWithError()
        //        .MaximumLengthWithError(Constraints.SHORT_TITLE_LENGTH);

        //    RuleFor(x => x.Description)
        //        .NotEmptyWithError()
        //        .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);

        //    RuleFor(x => x.BirthDate)
        //        .LessThanWithError(DateTimeOffset.UtcNow);

        //    RuleFor(x => x.Breed)
        //        .NotEmptyWithError()
        //        .MaximumLengthWithError(Constraints.SHORT_TITLE_LENGTH);

        //    RuleFor(x => x.Color)
        //        .NotEmptyWithError()
        //        .MaximumLengthWithError(Constraints.SHORT_TITLE_LENGTH);

        //    RuleFor(x => x.Place)
        //        .NotEmptyWithError()
        //        .MaximumLengthWithError(Constraints.SHORT_TITLE_LENGTH);

        //    RuleFor(x => x.PeopleAttitude)
        //        .NotEmptyWithError()
        //        .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);

        //    RuleFor(x => x.AnimalAttitude)
        //        .NotEmptyWithError()
        //        .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);

        //    RuleFor(x => x.Health)
        //        .NotEmptyWithError()
        //        .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);

        //    RuleFor(x => x.Height).GreaterThanWithError(0);
        //}
    }
}
