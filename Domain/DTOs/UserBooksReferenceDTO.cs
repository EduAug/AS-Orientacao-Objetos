using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS_Orientacao_Objetos.Domain.DTOs
{
    public class UserBooksReferenceDTO
    {
        public int UserId { get; set; }
        public int BookId { get; set; }

        // public UserDTO User { get; set; }
        // public BookDTO Book { get; set; }

        public DateTime RentalDate { get; set; }
        // public DateTime ReturnLimitDate { get; set; }
        public DateTime? ReturnedOn { get; set; }
    }
}