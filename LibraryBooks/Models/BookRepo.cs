using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace LibraryBooks.Models
{
    public class BookRepo
    {
        public void AddBook(Book book)
        {
            string sql = "Insert INTO Book(Title,Author, Price, Description, CountryId)" +
                    "VALUES(@Title,@Author, @Price, @Description, @CountryId)";
            using (IDbConnection connection = new SqlConnection(Helper.Constr("Books"))) 
            {
                connection.Query(sql, book).ToList();
            }
        }

        public void DeleteBook(int id)
        {
            using (IDbConnection connection = new SqlConnection(Helper.Constr("Books")))
            {
                connection.Execute("DELETE FROM Book where Id=@id", new { Id = id });
            }
        }
        public int AddBookReturnId(Book book)
        {
            string sql = "Insert INTO Book(Title,Author, Price, Description, CountryId)" +
                    "VALUES(@Title,@Author, @Price, @Description, @CountryId);" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
            using (IDbConnection connection = new SqlConnection(Helper.Constr("Books")))
            {
                var returnId = connection.Query<int>(sql, book).SingleOrDefault();
                return returnId;
            }
        }

        public List<Book> GetBooks()
        {
            using (IDbConnection connection = new SqlConnection(Helper.Constr("Books")))
            {
               return connection.Query<Book>("SELECT * From Book").ToList();
            }
        }

        public void UpdateBook(Book book)
        {
            using (IDbConnection connection = new SqlConnection(Helper.Constr("Books")))
            {
                connection.Execute("UPDATE Book SET Title=@title, Author=@author, " +
                     "Price=@price, Description=@description, CountryId=@countryid WHERE ID = @Id",
                     new { Title = book.Title, Author = book.Author, Price = book.Price, Description = book.Description, CountryId = book.CountryId, Id = book.Id });
            }
        }

        public List<Country> GetCountries()
        {
            using (IDbConnection connection = new SqlConnection(Helper.Constr("Books")))
            {
                return connection.Query<Country>("SELECT * From Country").ToList();
            }
        }

        public List<Book> GetAuthorsOfSelectedCountry(int id)
        {
            using (IDbConnection connection = new SqlConnection(Helper.Constr("Books")))
            {
                //return connection.Query<Book>("SELECT * From Book Where Country ").ToList();
                var list = connection.Query<Book>("SELECT Author from Book where CountryId=@id", 
                    new { Id = id }).ToList();
                return list;
            }
        }
    }
}
