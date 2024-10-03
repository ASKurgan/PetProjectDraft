using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.Common
{
    public class Result
    {
        public Result(bool isSucces, Error error)
        {
            if (isSucces && error != Error.None)
                throw new InvalidOperationException();

            if (!isSucces && error == Error.None)
                throw new InvalidOperationException();

            Error = error;
            IsSucces = isSucces;
        }

        public bool IsSucces { get;} 
        public Error Error { get;}
        public bool IsFailure => !IsSucces;

        public static Result Success() => new(true, Error.None);
        public static implicit operator Result(Error error) => new(false,error);
        
    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;
        public Result(TValue value, bool isSucces, Error error)
            :base(isSucces, error) 
        {
            _value = value;
        }

        public TValue Value => IsSucces
            ? _value 
            : throw new InvalidOperationException("The value of a failure, result can not be accessed") ;

        public static Result<TValue> Success(TValue value) => new(value, true, Error.None);

        public static implicit operator Result<TValue>(TValue value) => new(value, true, Error.None);

        public static implicit operator Result<TValue>(Error error) => new(default!, false, error);

    }
}
