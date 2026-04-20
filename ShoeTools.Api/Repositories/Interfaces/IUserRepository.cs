namespace ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Entities;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User> GetUserById(int id);
    Task <User> SaveAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);
}