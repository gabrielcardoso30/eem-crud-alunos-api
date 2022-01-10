using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Core.Helpers
{
    public static class EnumHelper
    {
        public static string ObterDescricaoEnum(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            
            return value.ToString();
        }
        /// <summary>
        /// Método que obtém o valor do atributo de descrição para eumeradors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ObterDescricaoEnum<T>(this T? value) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new Exception("Must be an enum.");

            if (!value.HasValue)
                return string.Empty;

            var fi = value.GetType().GetField(value.Value.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            
            return value.ToString();
        }

        /// <summary>
        /// Método que obtém a Descrição do Enum a partir do valor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ObterDescricaoEnum<T>(dynamic valor)
        {
            return Enum.GetName(typeof(T), Convert.ToInt16(valor));
        }
        
        public static List<EnumName> GetAllEnums()
        {
            var list = new List<Type>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assembliesEnums = assemblies.Where(p => p.FullName.Contains("Core"));

            foreach (var asse in assembliesEnums)
            {
                list.AddRange(asse.GetExportedTypes().Where(t => t.IsEnum));
            }

            list = list.Distinct().ToList();

            return list?.Where(w => w.Name != "LocalizedIdentityErrorMessages")
                .Select(item => item.Name).Select(dadosTabela => new EnumName { Nome = dadosTabela }).ToList();
        }

        public static List<EnumOutput<int>> GetValueEnum(string enumName)
        {
            var tabelaOutput = new List<EnumOutput<int>>();
            var list = new List<Type>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assembliesEnums = assemblies.Where(p => p.FullName.Contains("Core"));

            foreach (var asse in assembliesEnums)
            {
                list.AddRange(asse.GetExportedTypes().Where(t => t.IsEnum));
            }

            foreach (var enumValue in list?.Where(p => p.Name == enumName))
            {
                var dadosTabela = Enum.GetValues(enumValue);
                tabelaOutput.AddRange(from Enum item in dadosTabela select new EnumOutput<int> {Valor = Convert.ToInt32(item), Nome = item.ToString(), Descricao = item.Descricao()});
                break;
            }

            return tabelaOutput;
        }

        public static string Descricao(this Enum obj)
        {
            if (obj == null) return string.Empty;
            var customAttribute = Attribute.GetCustomAttribute(obj.GetType().GetField(obj.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute;
            return customAttribute == null ? obj.ToString() : customAttribute.Description;
        }

        public static string Valor(this Enum obj)
        {
            var aux = obj.ToString();
            return aux;
        }

        public static string Description<T>(this T enumerationValue) where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return enumerationValue.ToString();
        }

        public static string EnumDescription<T>(this T pValue)
        {
            if (pValue == null) return null;
            var field = pValue.GetType().GetField(pValue.ToString());
            var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
            return attribute != null ? ((DescriptionAttribute)attribute).Description : pValue.ToString();
        }

        public static EnumOutput<TValue> GetEnumOutput<TEnum, TValue>(TEnum pEnumValue)
        {
            if (pEnumValue == null)
                return null;

            var output = new EnumOutput<TValue>
            {
                Nome = pEnumValue.ToString(),
                Valor = (TValue)Convert.ChangeType((TEnum)pEnumValue, typeof(TValue)),
                Descricao = pEnumValue.EnumDescription<TEnum>()
            };

            return output;
        }


        public static T ToEnum<T>(this string pValue)
        {
            var type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

            if (!string.IsNullOrEmpty(pValue) && Enum.IsDefined(type, pValue))
            {
                return (T)Enum.Parse(type, pValue);
            }

            return default(T);
        }

        public static T ToEnum<T>(this EnumOutput<int> pValue)
        {
            return pValue == null ? default(T) : pValue.Nome.ToEnum<T>();
        }
        
        public class EnumOutput<T>
        {
            public string Nome { get; set; }
            public T Valor { get; set; }
            public string Descricao { get; set; }
        }
    
        public class EnumName
        {
            public string Nome { get; set; }
        }
    }
}
