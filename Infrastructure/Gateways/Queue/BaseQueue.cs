using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace Infrastructure.Gateways.Queue
{
    public class BaseQueue<T>(IConnection connection, string queue)
    {
        private readonly string _queue = queue;
        private readonly IConnection _connection = connection;
        public void Publish(T input)
        {
            using (var chanel = this._connection.CreateModel())
            {
                chanel.QueueDeclare(
                    queue: this._queue,
                    durable: true,
                    exclusive: true,
                    autoDelete: true,
                    arguments: null
                );

                var messaege = JsonSerializer.Serialize(
                    input
                );

                var body = Encoding.UTF8.GetBytes(messaege);

                chanel.BasicPublish(
                    exchange: "",
                    routingKey: this._queue,
                    basicProperties: null,
                    body: body
                );
            }
        }
    }
}
