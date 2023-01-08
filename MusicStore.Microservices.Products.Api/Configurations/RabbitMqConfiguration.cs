namespace MusicStore.Microservices.Products.Api.Configurations;

public class RabbitMqConfiguration
{
    public string Hostname { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(30);
}