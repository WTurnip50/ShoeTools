using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Api.Services.Interfaces;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    
    public async Task<bool> ClientExists(int clientId)
    {
        var client = await _clientRepository.GetClientById(clientId);
        return (client != null);
    }

    public async Task<ClientDto> SaveAsync(ClientDto client)
    {
        var dto = new Client
        {
            Name =  client.Name,
            PhoneNumber =  client.PhoneNumber,
            Email =  client.Email,
            CreatedBy = "",
            CreatedDate =  DateTime.Now,
            UpdatedBy = "",
            UpdatedDate =  DateTime.Now
        };
        dto = await _clientRepository.SaveAsync(dto);
        dto.Id = client.Id;
        return client;
    }

    public async Task<ClientDto> UpdateAsync(ClientDto client)
    {
        var dto = await _clientRepository.GetClientById(client.Id);
        if (dto == null)
        {
            throw new Exception("Client not found");
        }
        dto.Name = client.Name;
        dto.PhoneNumber = client.PhoneNumber;
        dto.Email = client.Email;
        dto.UpdatedBy = "";
        dto.UpdatedDate = DateTime.Now;
        await _clientRepository.UpdateAsync(dto);
        return client;
    }

    public async Task<List<ClientDto>> GetAllClients()
    {
        var clients = await _clientRepository.GetAllAsync();
        var clientsDto = clients.Select(client => new ClientDto(client)).ToList();
        return clientsDto;
    }

    public async Task<bool> DeleteAsync(int clientId)
    {
        return await _clientRepository.DeleteAsync(clientId);
    }

    public async Task<ClientDto> GetById(int clientId)
    {
        var client = await _clientRepository.GetClientById(clientId);
        if (client == null)
        {
            throw new Exception("Product not found");
        }

        var productDto = new ClientDto(client);
        return productDto;
    }
}