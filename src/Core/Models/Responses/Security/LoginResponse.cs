using System;
using System.Collections.Generic;
using Core.Entities.Security;

namespace Core.Models.Responses.Security
{

    public class LoginResponse
    {

        public Guid Id { get; set; }
        public Token Token { get; set; }
        public string Nome { get; set; }
        public bool PrimeiroLogin { get; set; }
        public bool TermoUso { get; set; }
        public bool Authenticated { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Menus { get; set; }
        public IEnumerable<string> MenusSubItens { get; set; }
        public string SelectedAccessUnitId { get; set; }       
        public string SelectedAccessUnitName { get; set; }
        public IEnumerable<KeyValuePair<string, string>> AvailableAccessUnits { get; set; } 
        
    }
    
}