using ShoeTools.Api.DataAccess.Interfaces;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Api.Services.Interfaces;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> UserExists(int userId)
    {
        var user = await _userRepository.GetUserById(userId);
        return user != null;
    }

    public async Task<AppUsersDto> SaveAsync(AppUsersDto user)
    {
        var appUser = new AppUsers
        {
            username =  user.username,
            password = user.password,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };
        appUser = await _userRepository.SaveAsync(appUser);
        return new AppUsersDto(appUser);
    }

    public async Task<AppUsersDto> UpdateAsync(AppUsersDto user)
    {
        var appUser = await _userRepository.GetUserById(user.Id);
        if (appUser == null)
        {
            throw new Exception("User not found");
        }
        appUser.username = user.username;
        appUser.password = user.password;
        appUser.UpdatedBy = "";
        appUser.UpdatedDate = DateTime.Now;
        await _userRepository.UpdateAsync(appUser);
        return user;
    }

    public async Task<List<AppUsersDto>> GetAllUsers()
    {
        var users = await _userRepository.GetAllAsync();
        var usersDto = users.Select(u=> new AppUsersDto(u)).ToList();
        return usersDto;
    }

    public async Task<bool> DeleteAsync(int userId)
    {
        return await _userRepository.DeleteAsync(userId);
    }

    public async Task<AppUsersDto> GetById(int userId)
    {
        var user = await _userRepository.GetUserById(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var userDto = new AppUsersDto(user);
        return userDto;
    }

    public async Task<AppUsersDto> LogIn(AppUsers user)
    {
        var userLogin = await _userRepository.LogIn(user);
        if (userLogin == null)
        {
            throw new Exception("User or credentials not valid.");
        }
        var userDto = new AppUsersDto(userLogin);
        return userDto;
    }
}