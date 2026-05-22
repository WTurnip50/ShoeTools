using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;

namespace ShoeTools.Website.Services.Interfaces;

public interface IUserService
{
    Task<Response<List<AppUsersDto>>> GetAllUsers();
    Task<Response<AppUsersDto>> GetUserById(int id);
    Task<Response<AppUsersDto>> SaveAsync(AppUsersDto usersDto);
    Task<Response<AppUsersDto>> UpdateAsync(AppUsersDto usersDto);
    Task<Response<bool>> Delete(int id);
    
    Task<Response<AppUsersDto>>LogIn(AppUsersDto user);
}