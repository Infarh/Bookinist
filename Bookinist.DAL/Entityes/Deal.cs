using System.ComponentModel.DataAnnotations.Schema;
using Bookinist.DAL.Entityes.Base;

namespace Bookinist.DAL.Entityes
{
    public class Deal : Entity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public Book Book { get; set; }

        public Seller Seller { get; set; }

        public Buyer Buyer { get; set; }

        public override string ToString() => $"Сделка по продаже {Book}: {Seller}, {Buyer}, {Price:C}";
    }
}