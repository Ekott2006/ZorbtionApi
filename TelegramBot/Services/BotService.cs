using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace TelegramBot.Services;

public class BotService(
    IConfiguration config,
    IServiceScopeFactory scopeFactory,
    ILogger<BotService> logger) : BackgroundService
{
    private readonly TelegramBotClient _bot =
        new(config["Key"] ?? throw new InvalidOperationException("API Key missing"));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Starting Telegram Bot...");

        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = [] // receive all update types
        };

        _bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            stoppingToken
        );

        User me = await _bot.GetMe(stoppingToken);
        logger.LogInformation("Bot started: @{MeUsername}", me.Username);

        // Keep the service alive
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        using IServiceScope scope = scopeFactory.CreateScope();
        UpdateHandler updateHandler = scope.ServiceProvider.GetRequiredService<UpdateHandler>();
        await updateHandler.HandleUpdateAsync(botClient, update, cancellationToken);
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Telegram Bot Error");
        return Task.CompletedTask;
    }
}