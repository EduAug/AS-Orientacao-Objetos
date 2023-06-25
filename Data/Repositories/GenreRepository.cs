using System.Linq;
using AS_Orientacao_Objetos.Data.Context;
using AS_Orientacao_Objetos.Domain.Entities;
using AS_Orientacao_Objetos.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AS_Orientacao_Objetos.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;
        public GenreRepository(DataContext context)
        {
            _context = context;
        }
        //-------------------------------
        public async Task<List<Genre>> GetAll()
        {
            var listAll = await _context.DbSetGenre.ToListAsync();
            foreach (var genres in listAll)
            {
                genres.Books = _context.DbSetBook.Where(g=>g.GenreId==genres.Id)
                .ToList();
            }
            return listAll;
        }
        public async Task<Genre> GetById(int id)
        {
            return await _context.DbSetGenre.FindAsync(id);
        }
        public bool Save(Genre entity)
        {
            //Aqui havia um método para conferir se a id do genero
            //já não existia, porém como a viewmodel pede apenas
            //pelo nome do gênero, e o mapper garante auto increment
            //não há o porquê, então simplesmente há adição
            _context.DbSetGenre.Add(entity);
            _context.SaveChanges();
            return true;
        }
        public bool Update(Genre entity)
        {
            var exists = GetById(entity.Id).Result;

            if(exists != null){
                exists.Name = entity.Name;
                _context.SaveChanges();
                return true;
            }else{
                return false;
            }
        }
        public bool Delete(int id)
        {
            var toDelete = GetById(id).Result;

            if(toDelete != null)
            {
                _context.DbSetGenre.Remove(toDelete);
                _context.SaveChanges();
                return true;
            }else{
                return false;
            }
        }
    }
}