# Zorbtion API

The backend API for the Zorbtion flashcard application.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- PostgreSQL (or configured database)

## Installation & Running

1. **Clone the repository** (if not already done).
2. **Navigate to the API directory**:
   ```bash
   cd Api
   ```
3. **Configure Database**:
   Ensure your `appsettings.json` has the correct connection string for your database.
4. **Run the API**:
   ```bash
   dotnet run
   ```
   The API will start, typically on `http://localhost:5000` or `https://localhost:5001`.

## Key Endpoints

- **Authentication**: `/api/Auth`
- **Decks**: `/api/Deck`
- **Cards**: `/api/Card`
- **Bot Authentication**: `/api/UserBotCode` (Used to generate tokens for the Telegram Bot)

## Configuration

The API is configured via `appsettings.json`. Key settings include:

- **ConnectionStrings**: Database connection details.
- **JwtSettings**: Configuration for JWT authentication.
- **Logging**: Log levels and outputs.

## Documentation

Interactive API documentation is available when running the API locally:

- **Scalar**: [https://localhost:7266/scalar](https://localhost:7266/scalar) - A modern, beautiful API reference.
- **Swagger UI**: [https://localhost:7266/swagger/index.html](https://localhost:7266/swagger/index.html) - Standard
  Swagger interface for testing endpoints.
