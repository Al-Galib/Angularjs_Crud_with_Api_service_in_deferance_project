using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Angularjs_Eve1.Models;
using Angularjs_Eve1.ViewModels;

namespace Angularjs_Eve1.Controllers
{
    [EnableCors("*", "*", "*")]
    public class BooksController : ApiController
    {
        private BookDbContext db = new BookDbContext();

        // GET: api/Books
        public IQueryable<Book> GetBooks()
        {
            return db.Books;
        }

        // GET: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }
        [HttpPost, Route("api/Create")]
        public IHttpActionResult Create(BookVm a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Book app = new Book { BookName = a.BookName, AuthorName = a.AuthorName, PublishDate = a.PublishDate, Picture = "no-pic.png" };
            string fileName = "";
            if (a.ImageType == "image/jpeg") fileName = Guid.NewGuid() + ".jpg";
            if (a.ImageType == "image/jpg") fileName = Guid.NewGuid() + ".jpg";
            if (a.ImageType == "image/png") fileName = Guid.NewGuid() + ".png";
            if (a.ImageType == "image/gif") fileName = Guid.NewGuid() + ".giv";

            byte[] bytes = Convert.FromBase64String(a.Picture);
          
            File.WriteAllBytes(Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), fileName), bytes);
            app.Picture = fileName;
            db.Books.Add(app);
            db.SaveChanges();

            return Ok(app);
        }
        [HttpPut, Route("api/Edit/{id}")]
        public IHttpActionResult Edit(int id, BookVm a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != a.Id)
            {
                return BadRequest();
            }

            var app = db.Books.First(x => x.Id == id);
            app.BookName = a.BookName;
            app.AuthorName = a.AuthorName;
            app.PublishDate = a.PublishDate;
            if (!string.IsNullOrEmpty(a.Picture))
            {
                string fileName = "";
                if (a.ImageType == "image/jpeg") fileName = Guid.NewGuid() + ".jpg";
                if (a.ImageType == "image/jpg") fileName = Guid.NewGuid() + ".jpg";
                if (a.ImageType == "image/png") fileName = Guid.NewGuid() + ".png";
                if (a.ImageType == "image/gif") fileName = Guid.NewGuid() + ".giv";

                byte[] bytes = Convert.FromBase64String(a.Picture);

                File.WriteAllBytes(Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), fileName), bytes);
                app.Picture = fileName;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(app);
        }
        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}