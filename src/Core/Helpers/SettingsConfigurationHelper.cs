using System;
using Microsoft.Extensions.Configuration;

namespace Core.Helpers
{

    public static class SettingsConfigurationHelper
    {

        public static string RetornaValor(string valor, IConfiguration configuration)
        {

            var valorRetorno = Environment.GetEnvironmentVariable(valor);

            if (string.IsNullOrEmpty(valorRetorno))
            {

                valorRetorno = configuration.GetValue<string>("APPSETTINGS__" + valor);
                if (string.IsNullOrEmpty(valorRetorno))
                {
                    valorRetorno = configuration.GetValue<string>("APPSETTINGS:" + valor);

                }

            }                

            return valorRetorno;

        }

    }

}