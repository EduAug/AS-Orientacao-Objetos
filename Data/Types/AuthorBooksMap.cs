using AS_Orientacao_Objetos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS_Orientacao_Objetos.Data.Types
{
    public class AuthorBooksMap : IEntityTypeConfiguration<AuthorBooks>
    {
        public void Configure(EntityTypeBuilder<AuthorBooks> builder)
        {
            builder.ToTable("author_books");

            builder.HasKey(k=>new {k.AuthorId, k.BookId});

            builder.HasOne<Author>(l=>l.Author)
            .WithMany(m=>m.AuthorBooks)
            .HasForeignKey(k=>k.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Book>(l=>l.Book)
            .WithMany(m=>m.AuthorBooks)
            .HasForeignKey(k=>k.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}