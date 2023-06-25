using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS_Orientacao_Objetos.Domain.Entities;

namespace AS_Orientacao_Objetos.Domain.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {

        Task<List<Book>> GetByGenre(int genreId);
        Task<List<Book>> GetByDonator(int userId);
        Task<List<Book>> GetAvailable();
    }
}