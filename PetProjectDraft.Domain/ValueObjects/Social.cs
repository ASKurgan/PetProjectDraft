﻿using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.ValueObjects
{
    // Данный класс - удобная замена enum-ам
    public class Social : ValueObject
    {
        public static readonly Social Telegram = new(nameof(Telegram).ToUpper());
        public static readonly Social Whatsapp = new(nameof(Whatsapp).ToUpper());
        public static readonly Social Viber = new(nameof(Viber).ToUpper());
        public static readonly Social Vkontakte = new(nameof(Vkontakte).ToUpper());
        public static readonly Social Odnoklassniki = new(nameof(Odnoklassniki).ToUpper());
        public static readonly Social Youtube = new(nameof(Youtube).ToUpper());
        public static readonly Social Tiktok = new(nameof(Tiktok).ToUpper());
        public static readonly Social Instagram = new(nameof(Instagram).ToUpper());

        private static readonly Social[] _all =
            [Telegram, Whatsapp, Viber, Vkontakte, Odnoklassniki, Youtube, Tiktok, Instagram];

        public string Value { get; }

        private Social(string value)
        {
            Value = value;
        }

        public static Result<Social> Create(string input)
        {
            input = input.Trim();

            if (input.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.SHORT_TITLE_LENGTH)
                return Errors.General.InvalidLength(nameof(Social));

            var social = input.ToUpper();

            if (_all.Any(s => s.Value == social) == false)
            {
                return Errors.General.ValueIsInvalid(nameof(Social));
            }

            return new Social(social);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
