using ClientsAPI.API.Models;

namespace ClientsAPI.API.Interfaces;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllClientsAsync();
    Task<Client?> GetClientByIdAsync(Guid id);
    Task<Client> CreateClientAsync(Client client);
}