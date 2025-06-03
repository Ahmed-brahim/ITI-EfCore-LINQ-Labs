using lab4._1_LibraryManagment_.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4._1_LibraryManagment_.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PriceOffer> PriceOffers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-DC6PHRTK\\SQLEXPRESS;Initial Catalog=lab4EF;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>(
                s =>
                {
                    //composite key
                    s.HasKey(x => new { x.BookId, x.AuthorId });

                    s.HasOne(x => x.Author)
                     .WithMany(x => x.bookAuthors)
                     .OnDelete(DeleteBehavior.Cascade);

                    s.HasOne(x => x.Book)
                     .WithMany(x => x.bookAuthors)
                     .OnDelete(DeleteBehavior.Cascade);
                }
            );

            modelBuilder.Entity<Review>(r =>
            {
                r.HasOne(x => x.Book)
                    .WithMany(x => x.Reviews)
                    .OnDelete(DeleteBehavior.Cascade);
            }
            );

            modelBuilder.Entity<PriceOffer>(r =>
            {
                r.HasOne(x => x.Book)
                    .WithOne(x => x.PriceOffer)
                    .OnDelete(DeleteBehavior.SetNull);
            }
            );
        }
    }
}
