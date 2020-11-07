using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bookinist.DAL.Entityes;
using Bookinist.Interfaces;

namespace Bookinist.Infrastructure.DebugServices
{
    class DebugBooksRepository : IRepository<Book>
    {
        public DebugBooksRepository()
        {
            var books = Enumerable.Range(1, 100)
               .Select(i => new Book
                {
                    Id = i,
                    Name = $"Книга {i}"
                })
               .ToArray();

            var categories = Enumerable.Range(1, 10)
               .Select(i => new Category
                {
                    Id = i,
                    Name = $"Категория {i}"
                })
               .ToArray();

            foreach (var book in books)
            {
                var category = categories[book.Id % categories.Length];
                category.Books.Add(book);
                book.Category = category;
            }

            Items = books.AsQueryable();
        }

        public IQueryable<Book> Items { get; }


        public Book Get(int id) { throw new NotImplementedException(); }

        public async Task<Book> GetAsync(int id, CancellationToken Cancel = default) { throw new NotImplementedException(); }

        public Book Add(Book item) { throw new NotImplementedException(); }

        public async Task<Book> AddAsync(Book item, CancellationToken Cancel = default) { throw new NotImplementedException(); }

        public void Update(Book item) { throw new NotImplementedException(); }

        public async Task UpdateAsync(Book item, CancellationToken Cancel = default) { throw new NotImplementedException(); }

        public void Remove(int id) { throw new NotImplementedException(); }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default) { throw new NotImplementedException(); }
    }
}
