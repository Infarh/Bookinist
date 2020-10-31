using Bookinist.DAL.Entityes;
using Bookinist.Interfaces;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        private readonly IRepository<Book> _Books;
        private readonly IRepository<Buyer> _Buyers;
        private readonly IRepository<Seller> _Sellers;

        public StatisticViewModel(IRepository<Book> Books, IRepository<Buyer> Buyers, IRepository<Seller> Sellers)
        {
            _Books = Books;
            _Buyers = Buyers;
            _Sellers = Sellers;
        }
    }
}