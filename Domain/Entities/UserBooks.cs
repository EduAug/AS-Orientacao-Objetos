using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS_Orientacao_Objetos.Domain.Entities
{
    public class UserBooks
    {
        //UserBooks é uma tabela "retirada", que controla
        //Quem tirou qual(is) livro(s) e quando.
        public int UserId { get; set; }
        public int BookId { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }

        public DateTime RentalDate { get; set; }
        public DateTime ReturnLimitDate { get; set; }
        public DateTime? ReturnedOn { get; set; }
    }
}