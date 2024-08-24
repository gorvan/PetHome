﻿namespace PetHome.Domain.Shared
{
    public static class Errors
    {
        public static class General
        {
            public static Error ValueIsInvalid(string? name = null)
            {
                var label = name ?? "value";
                return Error.Validation("value.is.invalid", $"{label} is invalid");
            }

            public static Error NotFound(Guid? id = null)
            {
                var forId = id == null ? "" : $" for id '{id}'";
                return Error.NotFound("record.not.found", $"record not found{forId}");
            }

            public static Error ValueIsRequeired(string? name = null)
            {
                var label = name == null ? "" : " " + name + " ";
                return Error.Validation("length.is.invalid", $"invalid{label}length");
            }

            public static Error AlreadyExist()
            {

                return Error.NotFound("record.already.exist",
                    "Volunteer already exist");
            }
        }
    }
}