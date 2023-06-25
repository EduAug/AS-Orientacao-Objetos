using System.Linq;
using AS_Orientacao_Objetos.Data.Context;
using AS_Orientacao_Objetos.Domain.Entities;
using AS_Orientacao_Objetos.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AS_Orientacao_Objetos.Data.Repositories
{
    public class AuthorBooksRepository : IAuthorBooksRepository<AuthorBooks>
    {
        private readonly DataContext _context;
        public AuthorBooksRepository(DataContext context)
        {
            _context = context;
        }
        //-------------------------------
        public async Task<List<AuthorBooks>> GetAll()
        {
            var listAllAB = await _context.DbSetAuthorBooks.ToListAsync();
            foreach (var listedRelation in listAllAB)
            {
                listedRelation.Author = _context.DbSetAuthor.SingleOrDefault(ab=>ab.Id==listedRelation.AuthorId);
                listedRelation.Book = _context.DbSetBook.SingleOrDefault(ab=>ab.Id==listedRelation.BookId);
            }
            return listAllAB;
        }
        public async Task<AuthorBooks> GetById(int idBook, int idAuthor)
        {
            //Retorna um authorbooks onde o id do livro e do autor
            //já são conhecidos, ou seja, onde autor e livro
            // se relacionam
            var foundAB = await _context.DbSetAuthorBooks.FirstOrDefaultAsync(a=>a.AuthorId==idAuthor&&a.BookId==idBook);
            foundAB.Author = _context.DbSetAuthor.FirstOrDefault(ab=>ab.Id==foundAB.AuthorId);
            foundAB.Book = _context.DbSetBook.FirstOrDefault(ab=>ab.Id==foundAB.BookId);
            return foundAB;
        }
        public bool Save(AuthorBooks entity)
        {
            entity.Author = _context.DbSetAuthor.SingleOrDefault(ab=>ab.Id==entity.AuthorId);
            entity.Book = _context.DbSetBook.SingleOrDefault(ab=>ab.Id==entity.BookId);
            _context.DbSetAuthorBooks.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public bool Update(AuthorBooks entity)
        {
            var exists = GetById(entity.BookId, entity.AuthorId).Result;
            
            if(exists != null){

                exists.AuthorId = entity.AuthorId;
                exists.BookId = entity.BookId;
                exists.Book = entity.Book;
                exists.Author = entity.Author;
                _context.SaveChanges();
                return true;
            }else{
                return false;
            }
        }
        public bool Delete(int IdB, int IdA)
        {
            var toDelete = _context.DbSetAuthorBooks.FirstOrDefault(ab=>ab.BookId==IdB&&ab.AuthorId==IdA);
            if(toDelete!=null)
            {
                _context.DbSetAuthorBooks.Remove(toDelete);
                _context.SaveChanges();
                return true;
            }else{
                return false;
            }
        }
        //-------------------------------
        public async Task<List<Book>> GetBooksByAuthor(int author)
        {
            var booksBy = await _context.DbSetAuthorBooks
            .Include(ab=>ab.Book)
                .ThenInclude(b=>b.DonatedBy)
            .Include(ab=>ab.Book)
                .ThenInclude(b=>b.Genre)
            .Include(ab=>ab.Book)
                .ThenInclude(b=>b.AuthorBooks)
            .Include(ab=>ab.Author)
            .Where(ab=>ab.AuthorId == author)
            .Select(ab=>ab.Book)
            .ToListAsync();
            foreach(var book in booksBy)
            {
                book.AuthorBooks = await _context.DbSetAuthorBooks
                    .Where(ab=>ab.BookId==book.Id)
                    .ToListAsync();
            }
            return booksBy;
        }
    }
}