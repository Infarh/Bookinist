using System.Linq;
using Bookinist.DAL.Context;
using Bookinist.DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Bookinist.DAL
{
    class BooksRepository : DbRepository<Book>
    {
        public override IQueryable<Book> Items => base.Items.Include(item => item.Category);

        public BooksRepository(BookinistDB db) : base(db) { }
    }
}