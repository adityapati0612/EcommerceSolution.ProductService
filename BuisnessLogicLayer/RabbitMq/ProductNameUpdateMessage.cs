namespace Ecommerce.BuisnessLogicLayer.RabbitMq;

public record ProductNameUpdateMessage(Guid ProductId, string? NewName);

