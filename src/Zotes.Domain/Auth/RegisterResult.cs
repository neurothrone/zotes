namespace Zotes.Domain.Auth;

public record RegisterResult
{
    public bool IsSuccess { get; private init; }
    public string? Error { get; private init; }
    public UserDto? User { get; private init; }

    public static RegisterResult Success(UserDto user) => new()
    {
        IsSuccess = true,
        User = user
    };

    public static RegisterResult Failure(string error) => new()
    {
        IsSuccess = false,
        Error = error
    };
}