using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Bookinist.DAL.Entityes;
using Bookinist.Infrastructure.DebugServices;
using Bookinist.Interfaces;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class BooksViewModel : ViewModel
    {
        private readonly IRepository<Book> _BooksRepository;

        #region BooksFilter : string - Искомое слово

        /// <summary>Искомое слово</summary>
        private string _BooksFilter;

        /// <summary>Искомое слово</summary>
        public string BooksFilter
        {
            get => _BooksFilter;
            set
            {
                if(Set(ref _BooksFilter, value))
                    _BooksViewSource.View.Refresh();
            }
        }

        #endregion

        private readonly CollectionViewSource _BooksViewSource;

        public ICollectionView BooksView => _BooksViewSource.View;

        public IEnumerable<Book> Books => _BooksRepository.Items;

        public BooksViewModel()
            :this(new DebugBooksRepository())
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера VisualStudio");
        }

        public BooksViewModel(IRepository<Book> BooksRepository)
        {
            _BooksRepository = BooksRepository;

            _BooksViewSource = new CollectionViewSource
            {
                Source = _BooksRepository.Items.ToArray(),
                SortDescriptions =
                {
                    new SortDescription(nameof(Book.Name), ListSortDirection.Ascending)
                }
            };

            _BooksViewSource.Filter += OnBooksFilter;
        }

        private void OnBooksFilter(object Sender, FilterEventArgs E)
        {
            if(!(E.Item is Book book) || string.IsNullOrEmpty(BooksFilter)) return;

            if (!book.Name.Contains(BooksFilter))
                E.Accepted = false;
        }
    }
}