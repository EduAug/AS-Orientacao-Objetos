using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS_Orientacao_Objetos.Domain.ViewModels
{
    public class AuthorBooksViewModel
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }

        // public AuthorViewModel Author { get; set; }
        // public BookViewModel Book { get; set; }
    }
}