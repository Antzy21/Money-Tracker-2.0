namespace MoneyTracker2.Utility;

public class Result<T>(bool isSuccess, T? value, string? errorMessage)
{
    public bool IsSuccess { get; } = isSuccess;
    public T? Value { get; } = value;
    public string? ErrorMessage { get; } = errorMessage;

    public static Result<T> Success(T value) => new Result<T>(true, value, null);
    public static Result<T> Failure(string errorMessage) => new Result<T>(false, default, errorMessage);
}