

namespace Core.Helpers
{

    public class AppSettings
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
        public string StorageAccountKey { get; set; }
        public string StorageAccountName { get; set; }
        public string StorageUrlBlobFiles { get; set; }
        public string StorageConnectionString { get; set; }

    }

}
