using FluentValidation;
using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Error = PetProjectDraft.Domain.Common.Error;

namespace PetProjectDraft.Application.ComonValidators
{
    public static class CustomValidators
    {

        public static IRuleBuilderOptions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
            this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject>> factoryMethod)
        {
            return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject> result = factoryMethod(value);

                if (result.IsSucces)
                    return;

                context.AddFailure(result.Error.Serialize());
            });
        }

        //public static IRuleBuilderOptions<T, IList<TElement>> 
        //    ListMustContainFewerThan<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
        //{
        //    return ruleBuilder.Must(list => list.Count < num).WithMessage("The list contains too many items");
        //}




        public static IRuleBuilderOptions<T, TProperty> NotEmptyWithError<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());
        }

        public static IRuleBuilderOptions<T, string> MaximumLengthWithError<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            int maxLength)
        {
            return ruleBuilder
                .MaximumLength(maxLength)
                .WithError(Errors.General.InvalidLength());
        }

        public static IRuleBuilderOptions<T, string> MinimumLengthWithError<T>(
          this IRuleBuilder<T, string> ruleBuilder,
          int minLength)
        {
            return ruleBuilder
                .MinimumLength(minLength)
                .WithError(Errors.General.InvalidLength());
        }

        public static IRuleBuilderOptions<T, TProperty> GreaterThanWithError<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder
                .GreaterThan(valueToCompare)
                .WithError(Errors.General.InvalidLength());
        }

        public static IRuleBuilderOptions<T, TProperty?> GreaterThanWithError<T, TProperty>(
            this IRuleBuilder<T, TProperty?> ruleBuilder, TProperty valueToCompare)
            where TProperty : struct, IComparable<TProperty>, IComparable
        {
            return ruleBuilder
                .GreaterThan(valueToCompare)
                .WithError(Errors.General.InvalidLength());
        }

        public static IRuleBuilderOptions<T, TProperty> LessThanWithError<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder
                .LessThan(valueToCompare)
                .WithError(Errors.General.InvalidLength());
        }
        // public static IRuleBuilderOptions<T, TProperty> MustBePhoto<T, TProperty>(
        //     this IRuleBuilder<T, TProperty> ruleBuilder, UploadVolunteerPhotoRequest file )
        //     where TProperty : IComparable<TProperty>, IComparable
        // {
        //     string[] allowedContentTypes = { "image/jpeg", "image/png", "image/png" };
        //
        //     var res = allowedContentTypes.Contains(file.File.ContentType);
        //     return ruleBuilder
        //         .Must(p => res)
        //         .WithError(Errors.General.ValueIsInvalid());
        //     
        // }
        public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, Error error)
        {
            return rule
                .WithMessage(error.Serialize());
        }
    }
}
