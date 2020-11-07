using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Bookinist.DAL.Context;
using Bookinist.DAL.Entityes;
using Bookinist.Interfaces;
using Bookinist.Models;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bookinist.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        private readonly IRepository<Book> _Books;
        private readonly IRepository<Buyer> _Buyers;
        private readonly IRepository<Seller> _Sellers;
        private readonly IRepository<Deal> _Deals;

        public ObservableCollection<BestSellerInfo> Bestsellers { get; } = new ObservableCollection<BestSellerInfo>();

        #region Command ComputeStatisticCommand - Вычислить статистические данные

        /// <summary>Вычислить статистические данные</summary>
        private ICommand _ComputeStatisticCommand;

        /// <summary>Вычислить статистические данные</summary>
        public ICommand ComputeStatisticCommand => _ComputeStatisticCommand
            ??= new LambdaCommandAsync(OnComputeStatisticCommandExecuted);

        /// <summary>Логика выполнения - Вычислить статистические данные</summary>
        private async Task OnComputeStatisticCommandExecuted()
        {
            await ComputeDealsStatisticAsync();
        }

        private async Task ComputeDealsStatisticAsync()
        {
            var bestsellers_query = _Deals.Items
               .GroupBy(b => b.Book.Id)
               .Select(deals => new { BookId = deals.Key, Count = deals.Count() })
               .OrderByDescending(deals => deals.Count)
               .Take(5)
               .Join(_Books.Items,
                    deals => deals.BookId,
                    book => book.Id,
                    (deals, book) => new BestSellerInfo { Book = book, SellCount = deals.Count });

            Bestsellers.Clear();
            foreach (var bestseller in await bestsellers_query.ToArrayAsync())
                Bestsellers.Add(bestseller);
        }

        #endregion

        public StatisticViewModel(
            IRepository<Book> Books,
            IRepository<Buyer> Buyers,
            IRepository<Seller> Sellers,
            IRepository<Deal> Deals)
        {
            _Books = Books;
            _Buyers = Buyers;
            _Sellers = Sellers;
            _Deals = Deals;
        }
    }
}