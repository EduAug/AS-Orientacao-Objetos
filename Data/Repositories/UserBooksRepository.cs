using System.Linq;
using AS_Orientacao_Objetos.Data.Context;
using AS_Orientacao_Objetos.Domain.Entities;
using AS_Orientacao_Objetos.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AS_Orientacao_Objetos.Data.Repositories
{
    public class UserBooksRepository : IUserBooksRepository<UserBooks>
    {
        private readonly DataContext _context;
        public UserBooksRepository(DataContext context)
        {
            _context = context;
        }
        //------------------------------
        public async Task<List<UserBooks>> GetAll()
        {
            var listAll = await _context.DbSetUserBooks.ToListAsync();
            foreach(var listedRelation in listAll)
            {
                listedRelation.Book = _context.DbSetBook.FirstOrDefault(b=>b.Id==listedRelation.BookId);
                listedRelation.User = _context.DbSetUser.FirstOrDefault(a=>a.Id==listedRelation.UserId);
            }
            return listAll;
        }

        public async Task<UserBooks> GetById(int idBook, int idUser)
        {
            var listThis = await _context.DbSetUserBooks.FirstOrDefaultAsync(a=>a.UserId==idUser&&a.BookId==idBook);
            if(listThis!=null)
            {
                listThis.Book = _context.DbSetBook.FirstOrDefault(b=>b.Id==listThis.BookId);
                listThis.User = _context.DbSetUser.FirstOrDefault(u=>u.Id==listThis.UserId);
            }
            return listThis;
        }
        //O processo de "alugar" um livro vai ser uma inserção
        //na "tabela" userbooks, que vai conter o usuário que alugou
        //e o livro alugado, tal qual as datas para isso.
        public bool Save(UserBooks entity)
        {
            var exists = _context.DbSetBook.FirstOrDefault(ub=>ub.Id==entity.BookId);
            // Como um usuário pode alugar o mesmo livro várias
            // vezes, e outros usuários podem alugar o mesmo
            // livro, não há o porquê de checar se já existe
            // essa entrada no histórico
            var existsUser = _context.DbSetUser.FirstOrDefault(ub=>ub.Id==entity.UserId);

            // Se achar o livro passado, e ele não estiver alugado
            // E se achar o usuário passado            (!exists.isRented)?
            if(exists != null && existsUser != null && exists.isRented==false)
            {
                entity.ReturnedOn = null;
                // Garante que o livro emprestado já não será
                // devolvido
                // Adiciona o novo "empréstimo"
                entity.Book = exists;
                entity.User = existsUser;
                entity.Book.isRented = true;
                //Ao alugar um livro, procura o livro da id passada
                //que graças ao Mapping já é relacionado 
                //e então muda sua disponibilidade para falsa,
                //ou se já está alugado para verdade
                _context.DbSetUserBooks.Add(entity);
                _context.SaveChanges();
                return true;
            }else{
                return false;
            }
        }
        public bool Update(UserBooks entity)
        {
            var exists = _context.DbSetUserBooks.FirstOrDefault(b=>b.BookId==entity.BookId&&b.UserId==entity.UserId);
            
            if(exists != null){

                exists.UserId = entity.UserId;
                exists.BookId = entity.BookId;
                exists.Book = entity.Book;
                exists.User = entity.User;
                exists.RentalDate = entity.RentalDate;
                exists.ReturnLimitDate = entity.ReturnLimitDate;
                if(entity.ReturnedOn.HasValue) //Hasvalue, outra forma do != null
                    //caso uma relação seja atualizada, mas
                    //o livro já tenha sido retornado
                    //(data de retorno já possua dados)
                    //então a nova data vai sobrescrever a antiga
                    //Do contrário, caso a devolução não tenha
                    //sido realizada, porém o empréstimo tenha
                    //de ser atualizado(data de retorno não existe)
                    //então atualiza os demais e ignora a data
                    //de retorno
                    exists.ReturnedOn = entity.ReturnedOn;
                _context.SaveChanges();
                return true;
            }else{
                return false;
            }
        }
        //Ao deletar uma entrada de empréstimo, por qualquer
        //razão que isso venha a se dar, uma vez que 
        //deletar um empréstimo NÃO será a devolução do(s) livro(s)
        //teremos de setar o livro da relação selecionada
        //como disponível novamente
        public bool Delete(int idBook, int idUser)
        {
            var toDelete = GetById(idBook,idUser).Result;
            var existsBook = _context.DbSetBook.FirstOrDefault(b=>b.Id==idBook);
            if(toDelete != null)
            {
                toDelete.Book = existsBook;
                existsBook.isRented = false;
                _context.DbSetUserBooks.Remove(toDelete);
                _context.SaveChanges();
                _context.Entry(existsBook).Reload();
                return true;
            }else{
                return false;
            }
        }
        //-------------------------------
        public async Task<List<UserBooks>> GetUsersRentingBooks()
        {
            return await _context.DbSetUserBooks
                .Include(ub=>ub.Book)
                .Include(ub=>ub.User)
                .Where(ub=>ub.Book.isRented)
                .ToListAsync();
        }
        public bool ReturnBook(int idBook, int idUser)
        {
            var rentalBook = _context.DbSetBook
                .FirstOrDefault(b=>b.Id==idBook&&b.isRented);
            var rentalUser = _context.DbSetUser
                .FirstOrDefault(b=>b.Id==idUser);
            var existsRented = _context.DbSetUserBooks
                .FirstOrDefault(i 
                => i.BookId == idBook 
                && i.UserId == idUser 
                && !i.ReturnedOn.HasValue);
            //Livro da ID passada por parâmetro, pelo usuário do parâmetro
            //cuja data de devolução ainda não tenha sido inserida

            if(rentalBook != null && existsRented != null)
            {
                existsRented.Book = rentalBook;
                existsRented.User = rentalUser;
                existsRented.ReturnedOn = DateTime.Now;

                rentalBook.isRented = false;
                
                _context.SaveChanges();
                _context.Entry(rentalBook).Reload();
                return true;
            }else{
                return false;
            }
        }
    }
}