using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Angularjs_Eve1.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required, StringLength(40)]
        public string BookName { get; set; }
        [Required, StringLength(40)]
        public string AuthorName { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime PublishDate { get; set; }
        [Required, StringLength(200)]
        public string Picture { get; set; }
    }
    public class BookDbContext : DbContext
    {
        public BookDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Book> Books { get; set; }
    }
    public class DbInitializer : DropCreateDatabaseIfModelChanges<BookDbContext>
    {
        protected override void Seed(BookDbContext context)
        {
            context.Books.Add(new Book { BookName = "Dejkin", AuthorName="Van", PublishDate = DateTime.Now.Date, Picture = "no-pic.png" });
            context.SaveChanges();
        }
    }
}