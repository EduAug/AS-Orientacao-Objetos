using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS_Orientacao_Objetos.Domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}