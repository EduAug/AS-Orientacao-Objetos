using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS_Orientacao_Objetos.Domain.Interfaces
{
    public interface IBaseRepository<Entity> where Entity : class
    {
        
        Task<Entity> GetById(int id);
        Task<List<Entity>> GetAll();
        bool Save(Entity entity);
        bool Update(Entity entity);
        bool Delete(int id);
    }
}