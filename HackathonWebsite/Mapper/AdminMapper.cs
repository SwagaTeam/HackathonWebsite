using HackathonWebsite.DataLayer.Entities;

namespace HackathonWebsite.Mapper;

public class AdminMapper
{
    public static AdminAuthDto AdminToDto(AdminEntity admin)
    {
        return new AdminAuthDto
        {
            Id = admin.Id,
            Nickname = admin.Nickname,
            Password = admin.Password,
        };
    }

    public static AdminEntity AdminToEntity(AdminAuthDto adminAuthDto, string salt)
    {
        return new AdminEntity
        {
            Id = (int)adminAuthDto.Id,
            Nickname = adminAuthDto.Nickname,
            Password = adminAuthDto.Password,
            Salt = salt
        };
    }
}