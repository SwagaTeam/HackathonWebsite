using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DTO;

namespace HackathonWebsite.Mapper;

public class UserMapper
{
    public static UserAuthDto UserToUserAuthDto(UserEntity user)
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

    public static UserEntity UserAuthDtoToUserEntity(UserAuthDto userAuthDto)
    {
        return new UserEntity
        {
            Id = userAuthDto.Id,
            FullName = userAuthDto.FullName,
            University = userAuthDto.University,
            Email = userAuthDto.Email,
            Password = userAuthDto.Password,
            TeamId = userAuthDto.TeamId,
            Telegram = userAuthDto.Telegram
        };
    }
}