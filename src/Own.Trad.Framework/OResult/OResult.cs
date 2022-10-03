using System;
using System.Collections.Generic;
using System.Linq;

namespace Own.Trad.Framework.OResult
{
    /// <summary>
    /// A discriminated union of errors or a value.
    /// </summary>
    public struct OResult<TValue> : IOResult
    {
        private readonly TValue _value;
        private readonly List<OError> _errors;

        private static readonly OError NoFirstError = OError.Unexpected(
            code: "ErrorOr.NoFirstError",
            description: "First error cannot be retrieved from a successful ErrorOr.");

        private static readonly OError NoErrors = OError.Unexpected(
            code: "ErrorOr.NoErrors",
            description: "Error list cannot be retrieved from a successful ErrorOr.");

        /// <summary>
        /// Gets a value indicating whether the state is error.
        /// </summary>
        public bool IsError { get; }

        /// <summary>
        /// Gets the list of errors.
        /// </summary>
        public List<OError> Errors => IsError ? _errors! : new List<OError> { NoErrors };

        /// <summary>
        /// Gets the value.
        /// </summary>
        public TValue Value => _value!;

        /// <summary>
        /// Gets the first error.
        /// </summary>
        public OError FirstError
        {
            get
            {
                if (!IsError)
                {
                    return NoFirstError;
                }

                return _errors![0];
            }
        }

        private OResult(OError error)
        {
            _value = default;
            _errors = new List<OError> { error };
            IsError = true;
        }

        private OResult(List<OError> errors)
        {
            _value = default;
            _errors = errors;
            IsError = true;
        }

        private OResult(TValue value)
        {
            _value = value;
            _errors = null;
            IsError = false;
        }

        /// <summary>
        /// Creates an <see cref="OResult{TValue}"/> from a value.
        /// </summary>
        public static implicit operator OResult<TValue>(TValue value)
        {
            return new OResult<TValue>(value);
        }

        /// <summary>
        /// Creates an <see cref="OResult{TValue}"/> from an error.
        /// </summary>
        public static implicit operator OResult<TValue>(OError error)
        {
            return new OResult<TValue>(error);
        }

        /// <summary>
        /// Creates an <see cref="OResult{TValue}"/> from a list of errors.
        /// </summary>
        public static implicit operator OResult<TValue>(List<OError> errors)
        {
            return new OResult<TValue>(errors);
        }

        /// <summary>
        /// Creates an <see cref="OResult{TValue}"/> from a list of errors.
        /// </summary>
        public static implicit operator OResult<TValue>(OError[] errors)
        {
            return new OResult<TValue>(errors.ToList());
        }

        public void Switch(Action<TValue> onValue, Action<List<OError>> onError)
        {
            if (IsError)
            {
                onError(Errors);
                return;
            }

            onValue(Value);
        }

        public void SwitchFirst(Action<TValue> onValue, Action<OError> onFirstError)
        {
            if (IsError)
            {
                onFirstError(FirstError);
                return;
            }

            onValue(Value);
        }

        public TResult Match<TResult>(Func<TValue, TResult> onValue, Func<List<OError>, TResult> onError)
        {
            if (IsError)
            {
                return onError(Errors);
            }

            return onValue(Value);
        }

        public TResult MatchFirst<TResult>(Func<TValue, TResult> onValue, Func<OError, TResult> onFirstError)
        {
            if (IsError)
            {
                return onFirstError(FirstError);
            }

            return onValue(Value);
        }
    }
}
