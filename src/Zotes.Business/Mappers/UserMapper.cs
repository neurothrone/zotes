using Zotes.Domain.Auth;
using Zotes.Persistence.Entities;

namespace Zotes.Business.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this UserEntity userEntity) => new(
        userEntity.Id,
        userEntity.Email ?? string.Empty,
        userEntity.FirstName,
        userEntity.LastName
    );

    public static UserEntity ToEntity(this RegisterRequest request) => new()
    {
        UserName = request.Email,
        Email = request.Email,
        FirstName = request.FirstName,
        LastName = request.LastName
    };
}