namespace Server.Application.Common;

public sealed record Result<T>(
    string? SuccessMessage,
    List<string>? ErrorMessages,
    T? Data);