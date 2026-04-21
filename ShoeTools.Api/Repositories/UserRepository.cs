using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using ShoeTools.Api.DataAccess.Interfaces;

namespace ShoeTools.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDBContext _dbContext;

    public UserRepository(IDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<AppUsers>> GetAllAsync()
    {
        const string sql = "SELECT * FROM AppUsers WHERE IsDeleted = 0";
        var users = await _dbContext.Connection.QueryAsync<AppUsers>(sql);
        return users.ToList();
    }

    public async Task<AppUsers> GetUserById(int id)
    {
        var user = await _dbContext.Connection.GetAsync<AppUsers>(id);
        if (user == null)
        {
            return null;
        }

        return user.IsDeleted == true ? null : user;
    }

    public async Task<AppUsers> SaveAsync(AppUsers appUsers)
    {
        appUsers.Id = await _dbContext.Connection.InsertAsync(appUsers);
        return appUsers;
    }

    public async Task<AppUsers> UpdateAsync(AppUsers appUsers)
    {
        await _dbContext.Connection.UpdateAsync(appUsers);
        return appUsers;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await GetUserById(id);
        if (user == null)
        {
            return false;
        }

        user.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(user);
    }
}