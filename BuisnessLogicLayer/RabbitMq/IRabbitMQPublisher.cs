namespace Ecommerce.BuisnessLogicLayer.RabbitMq;

public interface IRabbitMQPublisher
{
    void Publish<T>(string routingKey, T message);
}
