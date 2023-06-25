using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS_Orientacao_Objetos.Domain.DTOs
{
    public class AuthorBooksReferenceDTO
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        //Essa DTO só vai conter o id do autor e do livro,
        //e nada mais.
        
        // public string Author { get; set; }
        // public string Book { get; set; }
        // public AuthorDTO Author { get; set; }
        // public BookDTO Book { get; set; }
    }
}