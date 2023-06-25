using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS_Orientacao_Objetos.Domain.Entities;

namespace AS_Orientacao_Objetos.Domain.Interfaces
{
    public interface IAuthorBooksRepository<Entity> where Entity : class
    {
        //IAuthorBooksRepository não implementa IBaseRepository
        //uma vez que não consegue implementar o método
        //GetById com apenas uma Id, já que AuthorBooks não
        //possui um Id único por relação/par
        Task<AuthorBooks> GetById(int idBook, int idAuthor);
        Task<List<Entity>> GetAll();
        bool Save(Entity entity);
        bool Update(Entity entity);
        bool Delete(int idBook, int idAuthor);
        Task<List<Book>> GetBooksByAuthor(int author);
    }
}