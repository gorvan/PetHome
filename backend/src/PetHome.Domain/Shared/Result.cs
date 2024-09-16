﻿namespace PetHome.Domain.Shared
{
    public class Result
    {
        public Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public Error Error { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public static Result Success() => new(true, Error.None);


        public static implicit operator Result(Error error)
            => new(false, error);
    }

    public class Result<TValue> : Result
    {
        public Result(TValue value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        private readonly TValue _value;

        public TValue Value => IsSuccess
            ? _value
            : throw new InvalidOperationException("The value of a failure resualt can not be accessed");

        public static Result<TValue> Success(TValue value) => new(value, true, Error.None);

        public static implicit operator Result<TValue>(TValue value) => new(value, true, Error.None);
        public static implicit operator Result<TValue>(Error error) => new(default!, false, error);
    }
}
