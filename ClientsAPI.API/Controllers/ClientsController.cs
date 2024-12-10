using Microsoft.AspNetCore.Mvc;
using ClientsAPI.API.Interfaces;
using ClientsAPI.API.Models;

namespace ClientsAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientRepository _clientRepository;

    public ClientsController(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Client>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Client>>> GetClients()
    {
        var clients = await _clientRepository.GetAllClientsAsync();
        return Ok(clients);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Client>> GetClient(Guid id)
    {
        var client = await _clientRepository.GetClientByIdAsync(id);
        if (client == null)
        {
            return NotFound();
        }
        return Ok(client);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Client), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Client>> CreateClient(Client client)
    {
        client.Id = Guid.NewGuid();
        client.CreatedAt = DateTime.UtcNow;
        client.IsActive = true;

        var createdClient = await _clientRepository.CreateClientAsync(client);
        
        return CreatedAtAction(
            nameof(GetClient),
            new { id = createdClient.Id },
            createdClient);
    }
}