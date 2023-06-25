using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS_Orientacao_Objetos.Domain.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public List<UserBooksDTO> UserBooks { get; set; }
        // O "histórico de empréstimos" de um usuário ficará
        // salvo no usuário, e não no livro.
        public List<BookDTO> DonatedBooks { get; set; }
    }
}