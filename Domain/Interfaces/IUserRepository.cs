using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS_Orientacao_Objetos.Domain.Entities;

namespace AS_Orientacao_Objetos.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<List<User>> GetByName(string nameLike);
    }
}