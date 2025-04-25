using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DTO.Auth.UserAuth;

namespace HackathonWebsite.Mapper;

public class UserMapper
{
    public static UserAuthDto UserToDto(UserEntity user)
    {
        return new UserAuthDto
        {
            Id = user.Id,
            Email = user.Email,
            Password = user.Password,
            TeamId = user.TeamId,
            FullName = user.FullName,
            Telegram = user.Telegram,
            University = user.University
        };
    }

    public static UserEntity UserToEntity(UserAuthDto userAuthDto, string salt)
    {
        return new UserEntity
        {
            FullName = userAuthDto.FullName,
            University = userAuthDto.University,
            Email = userAuthDto.Email,
            Password = userAuthDto.Password,
            TeamId = userAuthDto.TeamId,
            Telegram = userAuthDto.Telegram,
            Salt = salt
        };
    }
}