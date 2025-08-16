using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Ecommerce.BuisnessLogicLayer.RabbitMq;

public class RabbitMqPublisher : IRabbitMQPublisher,IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IModel _channel;
    private readonly IConnection connection;
    public RabbitMqPublisher(IConfiguration configuration)
    {
        _configuration = configuration;
     
        string hostname = _configuration["RabbitMQ_HostName"]!;
        string userName = _configuration["RabbitMQ_UserName"]!;
        string password = _configuration["RabbitMQ_Password"]!;
        string port = _configuration["RabbitMQ_Port"]!;

        ConnectionFactory connectionFactory = new ConnectionFactory()
        {
            HostName = hostname,
            UserName = userName,
            Password = password,
            Port = Convert.ToInt32(port)
        };
         connection = connectionFactory.CreateConnection();

         _channel = connection.CreateModel();
    }

    public void Publish<T>(string routingKey, T message)
    {
        string messageJson= JsonSerializer.Serialize(message);
        byte[] messageBodyInBytes = Encoding.UTF8.GetBytes(messageJson);

        //create exchange
        string exchangeName = _configuration["RabbitMQ_Products_Exchange"]!;
        _channel.ExchangeDeclare(exchange: exchangeName,
                                 type: ExchangeType.Direct,
                                 durable: true);

        //publish message
        _channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: messageBodyInBytes);
    }

    public void Dispose()
    {
        _channel?.Dispose();
        connection?.Dispose();  
    }

}
