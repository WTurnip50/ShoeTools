using ShoeTools.Core.Dto;

namespace ShoeTools.Api.Services.Interfaces;

public interface IClientService
{
    Task<bool> ClientExists(int clientId);
    
    Task<ClientDto> SaveAsync(ClientDto client);
    
    Task<ClientDto> UpdateAsync(ClientDto client);
    
    Task<List<ClientDto>> GetAllClients();
    
    Task<bool> DeleteAsync(int clientId);
    
    Task<ClientDto> GetById(int clientId);
}