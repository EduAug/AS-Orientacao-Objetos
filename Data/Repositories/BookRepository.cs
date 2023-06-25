using System.Linq.Expressions;
using AS_Orientacao_Objetos.Data.Context;
using AS_Orientacao_Objetos.Domain.Entities;
using AS_Orientacao_Objetos.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
//Controler chama e dá o retorno, repository manipula
namespace AS_Orientacao_Objetos.Data.Repositories
{
    public class BookRepository :IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }
        //-------------------------------------------
        public async Task<List<Book>> GetAll()
        {
            var listAll = await _context.DbSetBook
            .Include(u=>u.AuthorBooks)
            .ToListAsync();
            foreach (var listedBook in listAll)
            {
                //Para evitar a "referência circular", gênero e doador
                //são ignorados pelo mapping na criação do Book, mas o DTO
                //pede os nomes do gênero e do doador para retornar à view
                //e não será possivel pegar 'Genre.Name' uma vez que não há genre
                //por isso, há aqui a "atribuição" de gênero e doador ao livro
                //que então retornam uma lista, e na controller são
                //transformados em DTOs, que pegam, pelo mapping, a entidade
                //gênero e doador, e pegam apenas o nome para retorno
                listedBook.Genre = _context.DbSetGenre.SingleOrDefault(b=>b.Id==listedBook.GenreId);
                listedBook.DonatedBy = _context.DbSetUser.SingleOrDefault(b=>b.Id==listedBook.DonatorId);
            }
            return listAll;
        }
        public async Task<Book> GetById(int id)
        {
            var returned = await _context.DbSetBook
                .Include(u=>u.AuthorBooks)
                .FirstOrDefaultAsync(u=>u.Id == id);
            returned.Genre = _context.DbSetGenre.SingleOrDefault(b=>b.Id==returned.GenreId);
            returned.DonatedBy = _context.DbSetUser.SingleOrDefault(b=>b.Id==returned.DonatorId);
            return returned;
        }
        public bool Save(Book entity)
        {
            entity.Genre =  _context.DbSetGenre.SingleOrDefault(b=>b.Id==entity.GenreId);
            entity.DonatedBy =  _context.DbSetUser.SingleOrDefault(b=>b.Id==entity.DonatorId);
            _context.DbSetBook.Add(entity);
            _context.SaveChanges();
            return true;
        }
        public bool Update(Book entity)
        {
            var exists = GetById(entity.Id).Result;

            if(exists == null)
            {
                return false;
            }else{
                exists.ISBN = entity.ISBN;
                exists.Title = entity.Title;
                exists.PageTotal = entity.PageTotal;
                exists.isRented = entity.isRented;
                exists.GenreId = entity.GenreId;
                exists.DonatorId = entity.DonatorId;

                exists.Genre = _context.DbSetGenre.SingleOrDefault(g=>g.Id==entity.GenreId);
                exists.DonatedBy = _context.DbSetUser.SingleOrDefault(u=>u.Id==entity.DonatorId);
                _context.SaveChanges();
                return true;
            }
        }
        public bool Delete(int id)
        {
            var toDelete = GetById(id).Result;

            if(toDelete != null)
            {
                _context.DbSetBook.Remove(toDelete);
                _context.SaveChanges();
                return true;
            }else{
                return false;
            }
        }
        //-------------------------------------
        public async Task<List<Book>> GetAvailable()
        {
            var listAvailable = await _context.DbSetBook
            .Include(u=>u.AuthorBooks)
            .Where(n=>!n.isRented)
            .ToListAsync();
            foreach (var availableBook in listAvailable)
            {
                availableBook.Genre = _context.DbSetGenre.SingleOrDefault(b=>b.Id==availableBook.GenreId);
                availableBook.DonatedBy = _context.DbSetUser.SingleOrDefault(b=>b.Id==availableBook.DonatorId);
            }
            return listAvailable;
            
        }
        public async Task<List<Book>> GetByDonator(int userId)
        {
            var listDonator = await _context.DbSetBook
            .Include(u=>u.AuthorBooks)
            .Where(i=>i.DonatorId == userId)
            .ToListAsync();
            foreach (var donatorBook in listDonator)
            {
                donatorBook.Genre = _context.DbSetGenre.SingleOrDefault(b=>b.Id==donatorBook.GenreId);
                donatorBook.DonatedBy = _context.DbSetUser.SingleOrDefault(b=>b.Id==donatorBook.DonatorId);
            }
            return listDonator;
        }
        public async Task<List<Book>> GetByGenre(int genreId)
        {
            var listGenre = await _context.DbSetBook
            .Include(u=>u.AuthorBooks)
            .Where(n=>n.GenreId==genreId)
            .ToListAsync();
            foreach (var genreBook in listGenre)
            {
                genreBook.Genre = _context.DbSetGenre.SingleOrDefault(b=>b.Id==genreBook.GenreId);
                genreBook.DonatedBy = _context.DbSetUser.SingleOrDefault(b=>b.Id==genreBook.DonatorId);
            }
            return listGenre;
        }
    }
}