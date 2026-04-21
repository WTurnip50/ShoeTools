using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Repositories.Interfaces;

public interface IClientRepository
{
    Task<List<Client>> GetAllAsync();
    Task<Client> GetClientById(int id);
    Task <Client> SaveAsync(Client client);
    Task<Client> UpdateAsync(Client client);
    Task<bool> DeleteAsync(int id);
}