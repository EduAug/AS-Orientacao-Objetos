using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS_Orientacao_Objetos.Domain.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public string WriterLicense { get; set; }
        public List<AuthorBooksReferenceDTO> AuthorBooks { get; set; }
    }
}