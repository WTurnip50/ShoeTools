using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;

namespace ShoeTools.Website.Services.Interfaces;

public interface IClientService
{
    Task<Response<List<ClientDto>>> GetAllClients();
    Task<Response<ClientDto>> GetClientById(int id);
    Task<Response<ClientDto>> SaveAsync(ClientDto clientDto);
    Task<Response<ClientDto>> UpdateAsync(ClientDto clientDto);
    Task<Response<bool>> Delete(int id);
}