using AS_Orientacao_Objetos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS_Orientacao_Objetos.Data.Types
{
    public class UserBooksMap : IEntityTypeConfiguration<UserBooks>
    {
        public void Configure(EntityTypeBuilder<UserBooks> builder)
        {
            builder.ToTable("user_books");

            builder.HasKey(k=>k.Id);

            builder.Property(j=>j.RentalDate)
            .HasColumnName("rental_date")
            .HasColumnType("DATETIME")
            .IsRequired();

            builder.Property(j=>j.ReturnLimitDate)
            .HasColumnName("rental_limit")
            .HasColumnType("DATETIME")
            .IsRequired();

            builder.Property(j=>j.ReturnedOn)
            .HasColumnName("returned_on")
            .HasColumnType("DATETIME");

            builder.HasOne<User>(l=>l.User)
            .WithMany(m=>m.UserBooks)
            .HasForeignKey(k=>k.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<Book>(l=>l.Book)
            .WithMany(m=>m.UserBooks)
            .HasForeignKey(k=>k.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}