using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Core.Entities.Security
{
    public class AuditoriaEntry
    {
        public AuditoriaEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string Entidade { get; set; }
        public string ParentKeyValue { get; set; }
        public string KeyValues { get; set; }
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();
        public Guid? AspNetUsersId { get; set; }
        public string EntityState { get; set; }

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public Auditoria ToAuditoria()
        {
            var auditoria = new Auditoria
            {
                Entidade = Entidade,
                DataEvento = DateTime.Now,
                KeyValue = KeyValues,
                ParentKeyValue = ParentKeyValue,
                OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
                NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues),
                AspNetUsersId = AspNetUsersId,
                EntityState = EntityState
            };
            
            return auditoria;
        }
    }
}