# Zorbtion Telegram Bot

A Telegram bot companion for the Zorbtion flashcard app, allowing you to study your cards directly from Telegram.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- A running instance of the **Zorbtion API**.
- A Telegram Bot Token (from [@BotFather](https://t.me/BotFather)).

## Installation & Setup

1. **Navigate to the Bot directory**:
   ```bash
   cd TelegramBot
   ```
2. **Configuration**:
   Open `appsettings.json` and configure your Bot Token and Database Connection:
   ```json
   {
     "Key": "YOUR_TELEGRAM_BOT_TOKEN",
     "ConnectionStrings": {
       "DefaultConnection": "YOUR_DATABASE_CONNECTION_STRING"
     }
   }
   ```
   *Note: The bot connects directly to the database for some operations and uses the Core services.*

3. **Run the Bot**:
   ```bash
   dotnet run
   ```

## Authentication

To use the bot, you must link your Telegram account to your Zorbtion user account.

1. **Get your Auth Token**:
   Make a request to the Zorbtion API endpoint:
   `GET {{ApiUrl}}/api/UserBotCode`

   *Replace `{{ApiUrl}}` with your running API URL (e.g., `http://localhost:5000`).*
   *You must be authenticated with the API to call this endpoint.*

2. **Link in Telegram**:
   Send the following command to the bot:
   ```
   /auth <YOUR_TOKEN>
   ```
   Example: `/auth 123456`

3. **Success**:
   The bot will confirm your login, and you can access the Dashboard and Decks.

## Features

- **Dashboard**: View your study streaks, retention rates, and deck summaries.
- **Study**: Review flashcards directly in the chat.
    - The bot shows the **Front** of the card.
    - **Reply** to the message with your answer.
    - The AI evaluates your answer and updates your progress.
