namespace Server.Application.Common;

public sealed record Result<T>(
    string? SuccessMessage,
    List<string>? ErrorMessages,
    T? Data)
{
    public bool IsSuccess => ErrorMessages == null || !ErrorMessages.Any();

    public static Result<T> Success(T data)
    {
        return new Result<T>(null, null, data);
    }

    public static Result<T> Success(string successMessage)
    {
        return new Result<T>(successMessage, null, default);
    }

    public static Result<T> Success(string successMessage, T data)
    {
        return new Result<T>(successMessage, null, data);
    }

    public static Result<T> Failure(string errorMessage)
    {
        return new Result<T>(null, new List<string>() { errorMessage }, default);
    }

    public static Result<T> Failure(List<string> errorMessages)
    {
        return new Result<T>(null, errorMessages, default);
    }
}