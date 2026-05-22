using ShoeTools.Core.Dto;
using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Services.Interfaces;

public interface IUserService
{
    Task<bool> UserExists(int userId);
    
    Task<AppUsersDto> SaveAsync(AppUsersDto user);
    
    Task<AppUsersDto> UpdateAsync(AppUsersDto user);
    
    Task<List<AppUsersDto>> GetAllUsers();
    
    Task<bool> DeleteAsync(int userId);
    
    Task<AppUsersDto> GetById(int userId);
    
    Task<AppUsersDto> LogIn(AppUsers user);
}