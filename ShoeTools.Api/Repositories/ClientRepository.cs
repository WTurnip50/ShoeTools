using Dapper;
using Dapper.Contrib.Extensions;
using ShoeTools.Api.DataAccess.Interfaces;
using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Repositories.Interfaces;

public class ClientRepository : IClientRepository
{
    private readonly IDBContext _dbContext;

    public ClientRepository(IDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Client>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Client WHERE IsDeleted = 0";
        var clients = await _dbContext.Connection.QueryAsync<Client>(sql);
        return clients.ToList();
    }

    public async Task<Client> GetClientById(int id)
    {
        var client = await _dbContext.Connection.GetAsync<Client>(id);
        if (client == null)
        {
            return null;
        }

        return client.IsDeleted == true ? null : client;
    }

    public async Task<Client> SaveAsync(Client client)
    {
        client.Id = await _dbContext.Connection.InsertAsync(client);
        return client;
    }

    public async Task<Client> UpdateAsync(Client client)
    {
        await _dbContext.Connection.UpdateAsync(client);
        return client;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var client = await GetClientById(id);
        if (client == null)
        {
            return false;
        }

        client.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(client);
    }
    
}