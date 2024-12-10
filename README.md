# Clients API

A .NET 9 Web API project for managing client information with JSON file-based storage.

## Features

- Get all clients
- Get client by ID
- Create new clients
- Data persistence using JSON file storage
- Swagger documentation

## Setup and Running

1. Clone the repository
```bash
git clone [your-repository-url]
cd ClientsAPI
```

2. Build the project
```bash
dotnet build
```

3. Run the application
```bash
dotnet run
```

The API will be available at `http://localhost:5029`

## API Documentation

Access Swagger UI at: `http://localhost:5029/swagger`

### Available Endpoints

- GET `/api/clients` - Retrieve all clients
- GET `/api/clients/{id}` - Retrieve a specific client by ID
- POST `/api/clients` - Create a new client

### Data Storage

The application uses a JSON file (`Data/clients.json`) for data persistence. The file is automatically created when the application runs for the first time.

### Sample Client Object
```json
{
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "Tim Apple",
    "email": "tim.apple@example.com",
    "phone": "+1234567890",
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": null,
    "isActive": true
}
```

### Improvements

- Update existing clients (PUT/PATCH endpoints)
- Delete clients
- Client data validation
- Search clients by name or email
- Pagination for large datasets
- Database integration (replacing JSON storage)
- Authentication and authorization
- Rate limiting
- Caching implementation
- API versioning

