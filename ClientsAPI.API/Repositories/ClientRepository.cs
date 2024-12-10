using System.Text.Json;
using ClientsAPI.API.Interfaces;
using ClientsAPI.API.Models;

namespace ClientsAPI.API.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly string _dbPath;

    public ClientRepository()
    {
        var projectPath = Directory.GetCurrentDirectory();
        _dbPath = Path.Combine(projectPath, "Data", "clients.json");

        var directory = Path.GetDirectoryName(_dbPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory!);
        }

        if (!File.Exists(_dbPath))
        {
            var emptyData = new ClientData { Clients = new List<Client>() };
            string jsonData = JsonSerializer.Serialize(emptyData, new JsonSerializerOptions 
            { 
                WriteIndented = true 
            });
            File.WriteAllText(_dbPath, jsonData);
        }
    }

    private async Task<List<Client>> LoadClientsAsync()
    {
        if (!File.Exists(_dbPath))
        {
            return new List<Client>();
        }

        var jsonContent = await File.ReadAllTextAsync(_dbPath);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
        
        var data = JsonSerializer.Deserialize<ClientData>(jsonContent, options);
        return data?.Clients ?? new List<Client>();
    }

    private async Task SaveClientsAsync(List<Client> clients)
    {
        var data = new ClientData { Clients = clients };
        var jsonContent = JsonSerializer.Serialize(data, new JsonSerializerOptions 
        { 
            WriteIndented = true 
        });
        await File.WriteAllTextAsync(_dbPath, jsonContent);
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await LoadClientsAsync();
    }

    public async Task<Client?> GetClientByIdAsync(Guid id)
    {
        var clients = await LoadClientsAsync();
        return clients.FirstOrDefault(c => c.Id == id);
    }

    public async Task<Client> CreateClientAsync(Client client)
    {
        var clients = await LoadClientsAsync();
        clients.Add(client);
        await SaveClientsAsync(clients);
        return client;
    }
}

public class ClientData
{
    public List<Client> Clients { get; set; } = new();
}