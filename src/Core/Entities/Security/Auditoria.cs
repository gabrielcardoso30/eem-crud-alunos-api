using System;

namespace Core.Entities.Security
{
    public class Auditoria
    {
        public Guid Id { get; set; }
        public string Entidade { get; set; }
        public DateTime DataEvento { get; set; }
        public string ParentKeyValue { get; set; }
        public string KeyValue { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string EntityState { get; set; }
        public Guid? AspNetUsersId { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
        
    }
}