using System.Collections.Generic;
using Bookinist.DAL.Entityes.Base;

namespace Bookinist.DAL.Entityes
{
    public class Category : NamedEntity
    {
        public virtual ICollection<Book> Books { get; set; }
    }
}