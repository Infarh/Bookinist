using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Bookinist.DAL.Entityes;
using Bookinist.Interfaces;
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

        #region BooksCount : int - Количество книг

        /// <summary>Количество книг</summary>
        private int _BooksCount;

        /// <summary>Количество книг</summary>
        public int BooksCount { get => _BooksCount; private set => Set(ref _BooksCount, value); }

        #endregion

        #region Command ComputeStatisticCommand - Вычислить статистические данные

        /// <summary>Вычислить статистические данные</summary>
        private ICommand _ComputeStatisticCommand;

        /// <summary>Вычислить статистические данные</summary>
        public ICommand ComputeStatisticCommand => _ComputeStatisticCommand
            ??= new LambdaCommandAsync(OnComputeStatisticCommandExecuted, CanComputeStatisticCommandExecute);

        /// <summary>Проверка возможности выполнения - Вычислить статистические данные</summary>
        private bool CanComputeStatisticCommandExecute() => true;

        /// <summary>Логика выполнения - Вычислить статистические данные</summary>
        private async Task OnComputeStatisticCommandExecuted()
        {
            BooksCount = await _Books.Items.CountAsync();

            var deals = _Deals.Items;

            var books = await deals.GroupBy(deal => deal.Book).Take(5).ToArrayAsync();

            var bestsellers = await deals.GroupBy(deal => deal.Book)
               .ToArrayAsync();
            //.Select(book_deals => new {Book = book_deals.Key, Count = book_deals.Count()})
            //.OrderByDescending(book => book.Count)
            //.Take(5)
            //.ToArrayAsync();
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