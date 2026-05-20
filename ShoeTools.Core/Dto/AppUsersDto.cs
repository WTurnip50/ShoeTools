using ShoeTools.Core.Entities;

namespace ShoeTools.Core.Dto;

public class AppUsersDto : DtoBase
{
    public string username { get; set; }
    public string password { get; set; }
    public bool IsActive { get; set; }
    
    public AppUsersDto()
    { }
    
    public  AppUsersDto(AppUsers appUsers)
    {
        Id = appUsers.Id;
        username = appUsers.username;
        password = appUsers.password;
        IsActive = appUsers.IsActive;
    }
}