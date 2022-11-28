using Chatroom.Models;
using Chatroom.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Chatroom.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        public RabbitMQService()
        {
        }

        public async Task SendMessage(ChatMessageViewModel message)
        {
            ConnectionFactory factory = GetConnectiofactory();

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var chat = $"chatQueue-{message.ChatRoom.Id}";
                channel.QueueDeclare(queue: chat,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                                     routingKey: chat,
                                     basicProperties: null,
                                     body: body);
            }
        }

        public ChatMessageViewModel? ConsumerRabbitMQ(int chatId)
        {
            ConnectionFactory factory = GetConnectiofactory();
            ChatMessageViewModel? chatMessageViewModel = new ChatMessageViewModel();

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var chat = $"chatQueue-{chatId}";

                channel.QueueDeclare(queue: chat,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    chatMessageViewModel = JsonSerializer.Deserialize<ChatMessageViewModel>(message);
                };

                channel.BasicConsume(queue: chat,
                                     autoAck: true,
                                     consumer: consumer);
            };

            return chatMessageViewModel;
        }

        private static ConnectionFactory GetConnectiofactory()
        {
            return new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "123456"
            };
        }
    }
}
