namespace CleanArchitecture.BlogManagement.Core.Base;
public sealed record Error (string ErrorCode, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
}
