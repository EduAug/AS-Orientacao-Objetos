using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS_Orientacao_Objetos.Domain.DTOs
{
    public class AuthorBooksDTO
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public string Author { get; set; }
        public string Book { get; set; }
        // public AuthorDTO Author { get; set; }
        // public BookDTO Book { get; set; }
    }
}