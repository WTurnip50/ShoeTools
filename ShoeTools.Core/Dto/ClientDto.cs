using ShoeTools.Core.Entities;

namespace ShoeTools.Core.Dto;

public class ClientDto : DtoBase
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public ClientDto()
    {
    }

    public ClientDto(Client clients)
    {
        Id = clients.Id;
        Name = clients.Name;
        PhoneNumber = clients.PhoneNumber;
        Email = clients.Email;
    }
}