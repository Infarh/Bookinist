using System.Windows.Input;
using Bookinist.DAL.Context;
using Bookinist.DAL.Entityes;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly BookinistDB _db;
        private readonly IRepository<Book> _Books;
        private readonly IRepository<Seller> _Sellers;
        private readonly IRepository<Buyer> _Buyers;
        private readonly IRepository<Deal> _Deals;
        private readonly ISalesService _SalesService;

        #region Title : string - Заголовок

        /// <summary>Заголовок</summary>
        private string _Title = "Главное окно программы";

        /// <summary>Заголовок</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region CurrentModel : ViewModel - Текущая дочерняя модель-представления

        /// <summary>Текущая дочерняя модель-представления</summary>
        private ViewModel _CurrentModel;

        /// <summary>Текущая дочерняя модель-представления</summary>
        public ViewModel CurrentModel { get => _CurrentModel; private set => Set(ref _CurrentModel, value); }

        #endregion

        #region Command ShowBooksViewCommand - Отобразить представление книг

        /// <summary>Отобразить представление книг</summary>
        private ICommand _ShowBooksViewCommand;

        /// <summary>Отобразить представление книг</summary>
        public ICommand ShowBooksViewCommand => _ShowBooksViewCommand
            ??= new LambdaCommand(OnShowBooksViewCommandExecuted, CanShowBooksViewCommandExecute);

        /// <summary>Проверка возможности выполнения - Отобразить представление книг</summary>
        private bool CanShowBooksViewCommandExecute() => true;

        /// <summary>Логика выполнения - Отобразить представление книг</summary>
        private void OnShowBooksViewCommandExecuted()
        {
            CurrentModel = new BooksViewModel(_Books);
        }

        #endregion

        #region Command ShowBuyersViewCommand - Отобразить представление покупателей

        /// <summary>Отобразить представление покупателей</summary>
        private ICommand _ShowBuyersViewCommand;

        /// <summary>Отобразить представление покупателей</summary>
        public ICommand ShowBuyersViewCommand => _ShowBuyersViewCommand
            ??= new LambdaCommand(OnShowBuyersViewCommandExecuted, CanShowBuyersViewCommandExecute);

        /// <summary>Проверка возможности выполнения - Отобразить представление покупателей</summary>
        private bool CanShowBuyersViewCommandExecute() => true;

        /// <summary>Логика выполнения - Отобразить представление покупателей</summary>
        private void OnShowBuyersViewCommandExecuted()
        {
            CurrentModel = new BuyersViewModel(_Buyers);
        }

        #endregion

        #region Command ShowStatisticViewCommand - Отобразить представление статистики

        /// <summary>Отобразить представление статистики</summary>
        private ICommand _ShowStatisticViewCommand;

        /// <summary>Отобразить представление статистики</summary>
        public ICommand ShowStatisticViewCommand => _ShowStatisticViewCommand
            ??= new LambdaCommand(OnShowStatisticViewCommandExecuted, CanShowStatisticViewCommandExecute);

        /// <summary>Проверка возможности выполнения - Отобразить представление статистики</summary>
        private bool CanShowStatisticViewCommandExecute() => true;

        /// <summary>Логика выполнения - Отобразить представление статистики</summary>
        private void OnShowStatisticViewCommandExecuted()
        {
            CurrentModel = new StatisticViewModel(
                _Books, _Buyers, _Sellers,
                _Deals
                );
        }

        #endregion

        public MainWindowViewModel(
            BookinistDB db,
            IRepository<Book> Books,
            IRepository<Seller> Sellers,
            IRepository<Buyer> Buyers,
            IRepository<Deal> Deals,
            ISalesService SalesService)
        {
            _db = db;
            _Books = Books;
            _Sellers = Sellers;
            _Buyers = Buyers;
            _Deals = Deals;
            _SalesService = SalesService;
        }

        //private async void Test()
        //{
        //    var deals_count = _SalesService.Deals.Count();

        //    var book = await _BooksRepository.GetAsync(5);
        //    var buyer = await _Buyers.GetAsync(3);
        //    var seller = await _Sellers.GetAsync(7);

        //    var deal = _SalesService.MakeADeal(book.Name, seller, buyer, 100m);

        //    var deals_count2 = _SalesService.Deals.Count();
        //}
    }
}
