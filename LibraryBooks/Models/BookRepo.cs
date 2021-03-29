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
    }
}
