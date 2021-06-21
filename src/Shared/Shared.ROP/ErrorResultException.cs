﻿using Shared.ROP.Extensions;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Shared.ROP
{
    class ErrorResultException : Exception
    {
        public ImmutableArray<Error> Errors { get; }

        public ErrorResultException(ImmutableArray<Error> errors)
            : base(ValidateAndGetErrorMessage(errors))
        {
            Errors = errors;
        }

        public ErrorResultException(Error error)
            : this(new[] { error }.ToImmutableArray())
        {
        }

        private static string ValidateAndGetErrorMessage(ImmutableArray<Error> errors)
        {
            if (errors.Length == 0)
            {
                throw new Exception("You must add at least one error");
            }

            if (errors.Length == 1)
            {
                return errors[0].Message;
            }

            return errors
                .Select(e => e.Message)
                .Prepend($"Has ocurred {errors.Length} Errores:")
                .JoinStrings(Environment.NewLine);
        }
    }
}