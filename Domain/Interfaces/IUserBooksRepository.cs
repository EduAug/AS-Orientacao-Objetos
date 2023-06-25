using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS_Orientacao_Objetos.Domain.Entities;

namespace AS_Orientacao_Objetos.Domain.Interfaces
{
    public interface IUserBooksRepository<Entity> where Entity : class
    {
        Task<Entity> GetById(int idBook, int idUser);
        Task<List<Entity>> GetAll();
        bool Save(Entity entity);
        bool Update(Entity entity);
        bool Delete(int idBook, int idUser);
        Task<List<UserBooks>> GetUsersRentingBooks();
        bool ReturnBook(int idBook, int idUser);
    }
}