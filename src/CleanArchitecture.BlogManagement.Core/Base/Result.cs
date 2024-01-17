namespace CleanArchitecture.BlogManagement.Core.Base;
public class Result<TValue>
{
    public bool IsSuccess { get; }
    public Error Error { get; }
    public TValue? Value { get; }

    private Result(TValue value)
    {
        IsSuccess = true;
        Value = value;
        Error = Error.None;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }

    public static implicit operator Result<TValue>(TValue value) => new(value);

    public static implicit operator Result<TValue>(Error error)
    {
        return error == null ? throw new ArgumentNullException(nameof(error), "Invalid error object. Error can not be null.") : new Result<TValue>(error);
    }
}
