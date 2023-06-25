using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS_Orientacao_Objetos.Domain.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int PageTotal { get; set; }
        public DateTime PublishDate { get; set; }
        public bool isRented { get; set; }
        public int GenreId { get; set; }
        public int DonatorId { get; set; }
        public string GenreName { get; set; }
        public string DonatorName { get; set; }
        // public GenreDTO Genre { get; set; }
        // public UserDTO DonatedBy { get; set; }
        // tava dando um problema danado de referência cruzada
        // no retorno, livro tem um gênero, que tem lista de livro, que tem gênero,...
        // então optei por, pelo menos no retorno, apenas retornar
        //os nomes daqueles objetos
        
        public List<AuthorBooksReferenceDTO> AuthorBooks { get; set; }
        // public List<UserBooksDTO> UserBooks { get; set; }
        // Estranho ter uma lista, o "histórico de aluguéis"
        // no livro, embora algumas bibliotecas tenham o cartãozinho
        // no final que contém nomes de quem retirou e data
    }
}