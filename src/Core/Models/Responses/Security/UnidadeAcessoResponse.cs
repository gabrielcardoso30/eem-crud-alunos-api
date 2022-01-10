using System;
using System.Collections.Generic;

namespace Core.Models.Responses.Security
{

    public class UnidadeAcessoResponse
    {

        public string Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Modulos { get; set; }
        public string SelectedAccessUnitId { get; set; }
        public string SelectedAccessUnitName { get; set; }
        public IEnumerable<KeyValuePair<string, string>> AvailableAccessUnits { get; set; }

    }

}
