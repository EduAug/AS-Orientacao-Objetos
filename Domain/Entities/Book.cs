using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS_Orientacao_Objetos.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int PageTotal { get; set; }
        public DateTime PublishDate { get; set; }
        public bool isRented { get; set; }
        public int GenreId { get; set; }
        public int DonatorId { get; set; }
        public Genre Genre { get; set; }
        public User DonatedBy { get; set; }
        //Um livro não pode existir sem estar na biblioteca,
        //E para isso, TEM de ser doado por um usuário
        public List<AuthorBooks> AuthorBooks { get; set; }
        public List<UserBooks> UserBooks { get; set; }
        //Então esse livro pode ser retirado por vários
        //Usuários por vir
    }
}