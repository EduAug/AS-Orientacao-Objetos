using System.Linq;
using AS_Orientacao_Objetos.Data.Context;
using AS_Orientacao_Objetos.Domain.Entities;
using AS_Orientacao_Objetos.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AS_Orientacao_Objetos.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        //--------------------------------
        public async Task<List<User>> GetAll()
        {
            return await _context.DbSetUser
            .Include(u=>u.DonatedBooks)
            .ToListAsync();
        }
        public async Task<User> GetById(int id)
        {
            return await _context.DbSetUser
                .Include(u=>u.DonatedBooks)
                .FirstOrDefaultAsync(u=>u.Id==id);
        }
        public bool Save(User entity)
        {
            _context.DbSetUser.Add(entity);
            _context.SaveChanges();
            return true;
        }
        public bool Update(User entity)
        {
            var exists = GetById(entity.Id).Result;

            if(exists != null){
                exists.Name = entity.Name;
                exists.Phone = entity.Phone;
                exists.CPF = entity.CPF;
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
                _context.DbSetUser.Remove(toDelete);
                _context.SaveChanges();
                return true;
            }else{
                return false;
            }
        }
        //-------------------------------------
        public async Task<List<User>> GetByName(string nameLike)
        {
            return await _context.DbSetUser
            .Include(u=>u.DonatedBooks)
            .Where(n=>n.Name.StartsWith(nameLike))
            .ToListAsync();
        }
    }
}