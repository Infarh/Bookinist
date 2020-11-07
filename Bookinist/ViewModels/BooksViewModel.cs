using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Bookinist.DAL.Entityes;
using Bookinist.Infrastructure.DebugServices;
using Bookinist.Interfaces;
using Bookinist.Services;
using Bookinist.Services.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bookinist.ViewModels
{
    class BooksViewModel : ViewModel
    {
        private readonly IRepository<Book> _BooksRepository;
        private readonly IUserDialog _UserDialog;

        #region Books : ObservableCollection<Book> - Коллекция книг

        /// <summary>Коллекция книг</summary>
        private ObservableCollection<Book> _Books;

        /// <summary>Коллекция книг</summary>
        public ObservableCollection<Book> Books
        {
            get => _Books;
            set
            {
                if (Set(ref _Books, value))
                {
                    _BooksViewSource = new CollectionViewSource
                    {
                        Source = value,
                        SortDescriptions =
                        {
                            new SortDescription(nameof(Book.Name), ListSortDirection.Ascending)
                        }
                    };

                    _BooksViewSource.Filter += OnBooksFilter;
                    _BooksViewSource.View.Refresh();

                    OnPropertyChanged(nameof(BooksView));
                }
            }
        }

        #endregion

        #region BooksFilter : string - Искомое слово

        /// <summary>Искомое слово</summary>
        private string _BooksFilter;

        /// <summary>Искомое слово</summary>
        public string BooksFilter
        {
            get => _BooksFilter;
            set
            {
                if (Set(ref _BooksFilter, value))
                    _BooksViewSource.View.Refresh();
            }
        }

        #endregion

        private CollectionViewSource _BooksViewSource;

        public ICollectionView BooksView => _BooksViewSource?.View;

        #region SelectedBook : Book - Выбранная книга

        /// <summary>Выбранная книга</summary>
        private Book _SelectedBook;

        /// <summary>Выбранная книга</summary>
        public Book SelectedBook { get => _SelectedBook; set => Set(ref _SelectedBook, value); }

        #endregion

        #region Command LoadDataCommand - Команда загрузки данных из репозитория

        /// <summary>Команда загрузки данных из репозитория</summary>
        private ICommand _LoadDataCommand;

        /// <summary>Команда загрузки данных из репозитория</summary>
        public ICommand LoadDataCommand => _LoadDataCommand
            ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);

        /// <summary>Проверка возможности выполнения - Команда загрузки данных из репозитория</summary>
        private bool CanLoadDataCommandExecute() => true;

        /// <summary>Логика выполнения - Команда загрузки данных из репозитория</summary>
        private async Task OnLoadDataCommandExecuted()
        {
            //Books = (await _BooksRepository.Items.ToArrayAsync()).ToObservableCollection();
            Books = new ObservableCollection<Book>(await _BooksRepository.Items.ToArrayAsync());
        }

        #endregion

        #region Command AddNewBookCommand - Добавление новой книги

        /// <summary>Добавление новой книги</summary>
        private ICommand _AddNewBookCommand;

        /// <summary>Добавление новой книги</summary>
        public ICommand AddNewBookCommand => _AddNewBookCommand
            ??= new LambdaCommand(OnAddNewBookCommandExecuted, CanAddNewBookCommandExecute);

        /// <summary>Проверка возможности выполнения - Добавление новой книги</summary>
        private bool CanAddNewBookCommandExecute() => true;

        /// <summary>Логика выполнения - Добавление новой книги</summary>
        private void OnAddNewBookCommandExecuted()
        {
            var new_book = new Book();

            if (!_UserDialog.Edit(new_book))
                return;

            _Books.Add(_BooksRepository.Add(new_book));

            SelectedBook = new_book;
        }

        #endregion

        #region Command RemoveBookCommand : Book - Удаление указанной книги

        /// <summary>Удаление указанной книги</summary>
        private ICommand _RemoveBookCommand;

        /// <summary>Удаление указанной книги</summary>
        public ICommand RemoveBookCommand => _RemoveBookCommand
            ??= new LambdaCommand<Book>(OnRemoveBookCommandExecuted, CanRemoveBookCommandExecute);

        /// <summary>Проверка возможности выполнения - Удаление указанной книги</summary>
        private bool CanRemoveBookCommandExecute(Book p) => p != null || SelectedBook != null;

        /// <summary>Проверка возможности выполнения - Удаление указанной книги</summary>
        private void OnRemoveBookCommandExecuted(Book p)
        {
            var book_to_remove = p ?? SelectedBook;

            if(!_UserDialog.ConfirmWarning($"Вы хотите удалить книгу {book_to_remove.Name}?", "Удаление книги"))
                return;

            _BooksRepository.Remove(book_to_remove.Id);

            Books.Remove(book_to_remove);
            if (ReferenceEquals(SelectedBook, book_to_remove))
                SelectedBook = null;
        }

        #endregion

        public BooksViewModel()
            : this(new DebugBooksRepository(), new UserDialogService())
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера VisualStudio");

            _ = OnLoadDataCommandExecuted();
        }

        public BooksViewModel(IRepository<Book> BooksRepository, IUserDialog UserDialog)
        {
            _BooksRepository = BooksRepository;
            _UserDialog = UserDialog;
        }

        private void OnBooksFilter(object Sender, FilterEventArgs E)
        {
            if (!(E.Item is Book book) || string.IsNullOrEmpty(BooksFilter)) return;

            if (!book.Name.Contains(BooksFilter))
                E.Accepted = false;
        }
    }
}