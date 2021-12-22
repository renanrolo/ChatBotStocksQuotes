using ChatBotStocksQuotes.Core.MessageBroker.Config;
using ChatBotStocksQuotes.Core.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections;
using System.Text;

namespace ChatBotStocksQuotes.Core.MessageBroker.Implementations
{
    public class RabbitMqUow : IDisposable
    {
        private IConnection _connection;
        private IModel _model;
        private IBasicProperties _basicProperties { get; set; }
        private IBasicProperties BasicProperties
        {
            get
            {
                if (_basicProperties == null)
                    _basicProperties = RabbitMQExtended.CreateBasicProperties(_model);

                return _basicProperties;
            }
        }

        protected readonly ConnectionFactory _connectionFactory;
        private readonly RabbitMqConfig _rabbitMqConfig;

        public IModel Chanel
        {
            get
            {
                if (_connection is null || !_connection.IsOpen)
                {
                    _connection = _connectionFactory.CreateConnection();
                }

                if (_model is null || !_model.IsOpen)
                {
                    _model = _connection.CreateModel();
                }

                return _model;
            }
        }

        public RabbitMqUow(RabbitMqConfig rabbitMqConfig)
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = rabbitMqConfig.HostName,
                Port = rabbitMqConfig.Port,
                UserName = rabbitMqConfig.UserName,
                Password = rabbitMqConfig.Password,
                VirtualHost = rabbitMqConfig.VirtualHost
            };

            Chanel.ExchangeDeclare(rabbitMqConfig.Exchange, "topic", true, false, null);
            _rabbitMqConfig = rabbitMqConfig;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_model?.IsClosed ?? false)
                {
                    _model.Close();
                }

                if (_connection?.IsOpen ?? false)
                {
                    _connection.Close();
                }
            }
        }

        public void ReadQueue<T>(string queueName, Action<T> callback)
        {
            while (Chanel.BasicGet(queueName, false) is BasicGetResult response && response != null)
            {
                T message = RabbitMQExtended.DeserializeResponse<T>(response.Body.ToArray());

                callback(message);

                Chanel.BasicAck(response.DeliveryTag, false);
            }
        }

        public void Push<T>(string topic, T data)
        {
            Chanel.ConfirmSelect();

            if (data is ICollection collection)
            {
                foreach (var item in collection)
                {
                    var jsonData = JsonConvert.SerializeObject(item);
                    Chanel.BasicPublish(_rabbitMqConfig.Exchange, topic, BasicProperties, Encoding.UTF8.GetBytes(jsonData));
                }
            }
            else
            {
                var jsonData = JsonConvert.SerializeObject(data);
                Chanel.BasicPublish(_rabbitMqConfig.Exchange, topic, BasicProperties, Encoding.UTF8.GetBytes(jsonData));
            }

            Chanel.WaitForConfirmsOrDie();
            Chanel.WaitForConfirms();
        }

        public string KeepListening<T>(string queueName, string consumerTag, Action<T> callback)
        {
            var consumer = new EventingBasicConsumer(Chanel);

            Chanel.BasicQos(0, 1, false);

            consumer.Received += (model, ea) =>
            {
                T message = RabbitMQExtended.DeserializeResponse<T>(ea.Body.ToArray());
                callback(message);
                Chanel.BasicAck(ea.DeliveryTag, false);
            };

            return Chanel.BasicConsume(queue: queueName,
                                       consumerTag: consumerTag,
                                       autoAck: false,
                                       consumer: consumer);
        }

        public void CancelListening(string consumerTag)
        {
            Chanel?.BasicCancelNoWait(consumerTag);

        }
    }

    internal class RabbitMQExtended
    {
        internal static T DeserializeResponse<T>(byte[] response)
        {
            var responseBody = Encoding.UTF8.GetString(response);
            return JsonConvert.DeserializeObject<T>(responseBody);
        }


        internal static IBasicProperties CreateBasicProperties(IModel model)
        {
            IBasicProperties basicProperties = model.CreateBasicProperties();
            basicProperties.ContentType = "text/plain";
            basicProperties.DeliveryMode = 2;
            basicProperties.Persistent = true;

            return basicProperties;
        }
    }
}
