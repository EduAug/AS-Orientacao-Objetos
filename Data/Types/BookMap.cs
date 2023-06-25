using AS_Orientacao_Objetos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS_Orientacao_Objetos.Data.Types
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("books");

            builder.Property(i=>i.Id)
            .HasColumnName("id")
            .IsRequired();
            builder.HasKey(i=>i.Id);

            builder.Property(i=>i.ISBN)
            .HasColumnName("isbn")
            .HasColumnType("CHAR(17)")
            .IsRequired();

            builder.Property(i=>i.Title)
            .HasColumnName("title")
            .HasColumnType("VARCHAR(70)")
            .IsRequired();

            builder.Property(i=>i.PageTotal)
            .HasColumnName("page_num")
            .HasColumnType("INT")
            .IsRequired();

            builder.Property(i=>i.PublishDate)
            .HasColumnName("publishing_date")
            .HasColumnType("DATETIME")
            .IsRequired();

            builder.Property(i=>i.isRented)
            .HasColumnName("is_rented")
            .HasColumnType("BOOLEAN")//.HasConversion<bool>()
            .IsRequired();

            builder.Property(i=>i.GenreId)
            .HasColumnName("genre")
            .IsRequired();

            builder.Property(i=>i.DonatorId)
            .HasColumnName("donator")
            .IsRequired();


            builder.HasOne(b=>b.Genre)
            .WithMany(g=>g.Books)
            .HasForeignKey(b=>b.GenreId);

            builder.HasOne(b=>b.DonatedBy)
            .WithMany(u=>u.DonatedBooks)
            .HasForeignKey(b=>b.DonatorId);


            builder.HasMany(b => b.UserBooks)
            .WithOne(ub=>ub.Book)
            .HasForeignKey(ub=>ub.BookId);

            builder.HasMany(b=>b.AuthorBooks)
            .WithOne(ab=>ab.Book)
            .HasForeignKey(ab=>ab.BookId);
        }
    }
}