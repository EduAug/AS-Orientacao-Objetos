using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Acredito que as viewmodels sejam usadas mais para a parte de
//update e create, uma vez que é um retorno da view/do front
namespace AS_Orientacao_Objetos.Domain.ViewModels
{
    public class AuthorViewModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public string WriterLicense { get; set; }
    }
}