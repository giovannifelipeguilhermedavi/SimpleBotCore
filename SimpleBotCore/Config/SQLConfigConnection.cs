namespace SimpleBotCore.Config
{
    public class SQLConfigConnection
    {
        public string AuthSource { get; set; }
        public string Database { get; set; } = "ChatBot2";
        public bool? Enabled { get; set; }
        public string Host { get; set; } = "MSSQLLocalDB";
        public string Password { get; set; }
        public int Port { get; set; } = 27017;
        public string ReplicaSet { get; set; }
        public bool RetryWrites { get; set; } = true;
        public string Username { get; set; }
        public bool UseSSL { get; set; }
        public string Write { get; set; }

        public string GetConnectionDefault()
        {
            return $"localdb://{Host}:{Port}/{Database}";
        }

        public string GetConnectionAtlas()
        {
            return $"localdb://{Username}:{Password}@{Host}/{Database}?ssl={UseSSL}&replicaSet={ReplicaSet}&authSource={AuthSource}&retryWrites={RetryWrites}&w={Write}";
        }
    }
}