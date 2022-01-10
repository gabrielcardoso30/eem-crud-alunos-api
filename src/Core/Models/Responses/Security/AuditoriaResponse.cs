using System;
using Core.Entities.Security;

namespace Core.Models.Responses.Security
{
    public class AuditoriaResponse
    {
        public Guid Id { get; set; }
        public string Entidade { get; set; }
        public DateTime DataEvento { get; set; }
        public string KeyValue { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public AspNetUsers Usuario { get; set; }
        public string Acao { get; set; }
    }
}
