using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookinist.DAL.Entityes;
using Bookinist.Interfaces;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Book> _BooksRepository;

        #region Title : string - Заголовок

        /// <summary>Заголовок</summary>
        private string _Title = "Главное окно программы";

        /// <summary>Заголовок</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        public MainWindowViewModel(IRepository<Book> BooksRepository)
        {
            _BooksRepository = BooksRepository;

            var books = BooksRepository.Items.Take(10).ToArray();
        }
    }
}
