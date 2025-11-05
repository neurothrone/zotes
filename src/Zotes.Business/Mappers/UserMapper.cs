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
}