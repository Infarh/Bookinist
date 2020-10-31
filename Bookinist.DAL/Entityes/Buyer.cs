using Bookinist.DAL.Entityes.Base;

namespace Bookinist.DAL.Entityes
{
    public class Buyer : Person
    {
        public override string ToString() => $"Покупатель {Surname} {Name} {Patronymic}";
    }
}