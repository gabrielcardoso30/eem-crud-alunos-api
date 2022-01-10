using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Core.Helpers
{
    public class JsonIgnoreHelper : DefaultContractResolver
    {
        /*
        public static void IgnoreProperty<T, TR>(T parameter, Expression<Func<T, TR>> propertyLambda)
        {
            var parameterType = parameter.GetType();

            var member = propertyLambda.Body as MemberExpression;
            var memberPropertyInfo = member?.Member as PropertyInfo;
            var propertyName = memberPropertyInfo?.Name;
            if (propertyName == null)
            {
                return;
            }
            
            var jsonPropertyAttribute = parameterType.GetProperty(propertyName).GetCustomAttribute<JsonPropertyAttribute>();
            jsonPropertyAttribute.DefaultValueHandling = DefaultValueHandling.Ignore;
        }
        */
        
        private readonly List<string> _props;

        public JsonIgnoreHelper(IEnumerable<string> prop) {
            _props = prop.ToList();
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization) {
            
            var retval = base.CreateProperties(type, memberSerialization);

            // retorna todas as propriedades que não estão na lista para ignorar
            retval = retval.Where(p => !_props.Contains(p.PropertyName)).ToList();

            return retval;
        }
        
    }
}