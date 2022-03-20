namespace ChatBotStocksQuotes.Core.MessageBroker.Config
{
    public class RabbitMqConfig
    {
        public string Exchange { get; set; }
        public int QueueMaxLength { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
    }
}
