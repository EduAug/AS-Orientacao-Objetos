using System.Linq;
using AS_Orientacao_Objetos.Data.Context;
using AS_Orientacao_Objetos.Domain.Entities;
using AS_Orientacao_Objetos.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AS_Orientacao_Objetos.Data.Repositories
{
    public class AuthorRepository : IAuthoRepository
    {
        private readonly DataContext _context;
        public AuthorRepository(DataContext context)
        {
            _context = context;
        }
        //-------------------------------------------
        public async Task<List<Author>> GetAll()
        {
            return await _context.DbSetAuthor
            .Include(u=>u.AuthorBooks)
            .ToListAsync();
        }
        public async Task<Author> GetById(int id)
        {
            return await _context.DbSetAuthor
                .Include(u=>u.AuthorBooks)
                .FirstOrDefaultAsync(u=>u.Id==id);
        }
        public bool Save(Author entity)
        {
            _context.DbSetAuthor.Add(entity);
            _context.SaveChanges();
            return true;
        }
        public bool Update(Author entity)
        {
            var exists = GetById(entity.Id).Result;

            if(exists != null){
                exists.Name = entity.Name;
                exists.Phone = entity.Phone;
                exists.CPF = entity.CPF;
                exists.WriterLicense = entity.WriterLicense;
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
                _context.DbSetAuthor.Remove(toDelete);
                _context.SaveChanges();
                return true;
            }else{
                return false;
            }
        }
    }
}