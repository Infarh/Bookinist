using Bookinist.DAL.Entityes.Base;

namespace Bookinist.DAL.Entityes
{
    public class Seller : Person
    {
        public override string ToString() => $"Продавец {Surname} {Name} {Patronymic}";
    }
}