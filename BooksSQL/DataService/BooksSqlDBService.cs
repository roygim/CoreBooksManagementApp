using BooksSQL.DataContexts;
using BooksSQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksSQL.DataService
{
    public class BooksSqlDBService
    {
        public static IEnumerable<Books> GetAllBooks(BooksSqlDBContext booksContext)
        {
            return booksContext.Books;
        }

        public static Books GetBookById(BooksSqlDBContext booksContext, int Id)
        {
            return booksContext.Books.Find(Id);
        }

        public static Books AddBook(BooksSqlDBContext booksContext, Books newBook)
        {
            booksContext.Books.Add(newBook);
            booksContext.SaveChanges();
            return newBook;
        }

        public static Books UpdateBook(BooksSqlDBContext booksContext, int id, Books book)
        {
            book.Id = id;
            booksContext.Entry(book).State = EntityState.Modified;
            booksContext.SaveChanges();
            return book;
        }

        public static async Task<Books> DeleteBook(BooksSqlDBContext booksContext, int id)
        {
            Books book = await booksContext.Books.FindAsync(id);
            if (book == null)
            {
                return null;
            }

            booksContext.Books.Remove(book);
            await booksContext.SaveChangesAsync();

            return book;
        }

        public static IEnumerable<Books> GetStoredProcedureExample(BooksSqlDBContext booksContext, Books book)
        {
            return booksContext.Books.FromSqlRaw<Books>("GetBookByName @Name={0}, @Year={1}", book.Name, book.Year);
        }
    }
}
