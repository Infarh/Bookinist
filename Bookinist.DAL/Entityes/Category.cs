using System.Collections.Generic;
using Bookinist.DAL.Entityes.Base;

namespace Bookinist.DAL.Entityes
{
    public class Category : NamedEntity
    {
        public ICollection<Book> Books { get; set; } = new HashSet<Book>();

        public override string ToString() => $"Категория {Name}";
    }
}