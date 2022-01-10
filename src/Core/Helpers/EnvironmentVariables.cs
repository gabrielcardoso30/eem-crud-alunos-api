namespace Core.Helpers
{

    public class EnvironmentVariables
    {

        public string KafkaServer { get; set; }
        public string KafkaPort { get; set; }
        public string KafkaMailTopic { get; set; }
        public string QuantidadeLogin { get; set; }
        public string TokenAudience { get; set; }
        public string TokenIssuer { get; set; }
        public string TokenSeconds { get; set; }
        public string SmtpMailAccount { get; set; }
        public string SmtpMailPassword { get; set; }
        public string SmtpMailDisplayName { get; set; }
        public string SmtpMailHost { get; set; }
        public string SmtpMailPort { get; set; }
        public string StorageAccountKey   { get; set; }
        public string StorageAccountName  { get; set; }
        public string StorageUrlBlobFiles { get; set; }
        public string StorageConnectionString { get; set; }

        public EnvironmentVariables(
            string kafkaServer, 
            string kafkaPort, 
            string kafkaMailTopic,
            string quantidadeLogin, 
            string tokenAudience, 
            string tokenIssuer, 
            string tokenSeconds,
            string mailAccount,
            string mailPassword,
            string mailDisplayName,
            string mailHost,
            string mailPort,
            string storageAccountKey,
            string storageAccountName,
            string storageUrlBlobFiles,
            string storageConnectionString
        )
        {
            KafkaServer = kafkaServer;
            KafkaPort = kafkaPort;
            KafkaMailTopic = kafkaMailTopic;
            QuantidadeLogin = quantidadeLogin;
            TokenAudience = tokenAudience;
            TokenIssuer = tokenIssuer;
            TokenSeconds = tokenSeconds;
            SmtpMailAccount = mailAccount;
            SmtpMailPassword = mailPassword;
            SmtpMailDisplayName = mailDisplayName;
            SmtpMailHost = mailHost;
            SmtpMailPort = mailPort;
            StorageAccountKey = storageAccountKey;
            StorageAccountName = storageAccountName;
            StorageUrlBlobFiles = storageUrlBlobFiles;
            StorageConnectionString = storageConnectionString;
        }

    }

}
