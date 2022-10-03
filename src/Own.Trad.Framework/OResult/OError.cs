namespace Own.Trad.Framework.OResult
{
    /// <summary>
    /// Represents an error.
    /// </summary>
    public readonly struct OError
    {
        /// <summary>
        /// Gets the unique error code.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Gets the error description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the error type.
        /// </summary>
        public OErrorType Type { get; }

        /// <summary>
        /// Gets the numeric value of the type.
        /// </summary>
        public int NumericType { get; }

        private OError(string code, string description, OErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
            NumericType = (int)type;
        }

        /// <summary>
        /// Creates an <see cref="OError"/> of type <see cref="OErrorType.Failure"/> from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        public static OError Failure(
            string code = "General.Failure",
            string description = "A failure has occurred.") =>
            new OError(code, description, OErrorType.Failure);

        /// <summary>
        /// Creates an <see cref="OError"/> of type <see cref="OErrorType.Unexpected"/> from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        public static OError Unexpected(
            string code = "General.Unexpected",
            string description = "An unexpected error has occurred.") =>
            new OError(code, description, OErrorType.Unexpected);

        /// <summary>
        /// Creates an <see cref="OError"/> of type <see cref="OErrorType.Validation"/> from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        public static OError Validation(
            string code = "General.Validation",
            string description = "A validation error has occurred.") =>
            new OError(code, description, OErrorType.Validation);

        /// <summary>
        /// Creates an <see cref="OError"/> of type <see cref="OErrorType.Conflict"/> from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        public static OError Conflict(
            string code = "General.Conflict",
            string description = "A conflict error has occurred.") =>
            new OError(code, description, OErrorType.Conflict);

        /// <summary>
        /// Creates an <see cref="OError"/> of type <see cref="OErrorType.NotFound"/> from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        public static OError NotFound(
            string code = "General.NotFound",
            string description = "A 'Not Found' error has occurred.") =>
            new OError(code, description, OErrorType.NotFound);

        /// <summary>
        /// Creates an <see cref="OError"/> with the given numeric <paramref name="type"/>,
        /// <paramref name="code"/>, and <paramref name="description"/>.
        /// </summary>
        /// <param name="type">An integer value which represents the type of error that occurred.</param>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        public static OError Custom(
            int type,
            string code,
            string description) =>
            new OError(code, description, (OErrorType)type);
    }
}