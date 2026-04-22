namespace ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Entities;

public interface IUserRepository
{
    Task<List<AppUsers>> GetAllAsync();
    Task<AppUsers> GetUserById(int id);
    Task <AppUsers> SaveAsync(AppUsers appUsers);
    Task<AppUsers> UpdateAsync(AppUsers appUsers);
    Task<bool> DeleteAsync(int id);
    
}

//Task<AppUsers> Login(AppUsers user);