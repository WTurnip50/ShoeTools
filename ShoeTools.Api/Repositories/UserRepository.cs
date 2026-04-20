using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Repositories;

public class UserRepository : IUserRepository
{
    public async Task<List<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> SaveAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}