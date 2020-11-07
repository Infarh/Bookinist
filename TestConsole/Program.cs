using System.Linq;
using Bookinist.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Bookinist.db;Integrated Security=True";
            using var db = new BookinistDB(new DbContextOptionsBuilder<BookinistDB>().UseSqlServer(connection).Options);

            var query = db.Deals
               .Include(d => d.Book)
               //.AsEnumerable()
               .GroupBy(d => d.Book.Id, d => d.Book)
               .Select(d => new { BookId = d.Key, Count = d.Count() })
               .Join(db.Books, d => d.BookId, b => b.Id, (d, Book) => new { Book, d.Count })
               .OrderByDescending(d => d.Count);

            var sql = query.ToQueryString();

            var result = query.ToArray();
        }
    }


}
