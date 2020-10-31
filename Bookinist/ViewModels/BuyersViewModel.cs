using Bookinist.DAL.Entityes;
using Bookinist.Interfaces;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class BuyersViewModel : ViewModel
    {
        private readonly IRepository<Buyer> _Buyers;

        public BuyersViewModel(IRepository<Buyer> Buyers)
        {
            _Buyers = Buyers;
        }
    }
}
