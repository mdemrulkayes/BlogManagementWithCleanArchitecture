namespace CleanArchitecture.BlogManagement.Core.Base;
public sealed record Error (string ErrorCode, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static Error Failure(
        string code = "Common.Failure",
        string description = "A failure has occurred.") =>
        new(code, description);

    public static Error Unexpected(
        string code = "General.Unexpected",
        string description = "An unexpected error has occurred.") =>
        new(code, description);

    public static Error Validation(
        string code = "Common.Validation",
        string description = "A validation error has occurred.") =>
        new(code, description);

    public static Error Conflict(
        string code = "Common.Conflict",
        string description = "A conflict error has occurred.") =>
        new(code, description);

    public static Error NotFound(
        string code = "General.NotFound",
        string description = "A 'Not Found' error has occurred.") =>
        new(code, description);

    public static Error Unauthorized(
        string code = "General.Unauthorized",
        string description = "An 'Unauthorized' error has occurred.") =>
        new(code, description);
}
