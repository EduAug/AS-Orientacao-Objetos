using AS_Orientacao_Objetos.Data.Types;
using AS_Orientacao_Objetos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AS_Orientacao_Objetos.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}

        public DbSet<Author> DbSetAuthor { get; set; }
        public DbSet<Book> DbSetBook { get; set; }
        public DbSet<User> DbSetUser { get; set; }
        public DbSet<Genre> DbSetGenre { get; set; }
        public DbSet<AuthorBooks> DbSetAuthorBooks { get; set; }
        public DbSet<UserBooks> DbSetUserBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AuthorMap());
            builder.ApplyConfiguration(new BookMap());
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new GenreMap());
            builder.ApplyConfiguration(new AuthorBooksMap());
            builder.ApplyConfiguration(new UserBooksMap());
        }
    }
}