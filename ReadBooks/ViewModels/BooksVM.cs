using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using ReadBooks.Models;
using ReadBooks.Views;
using SQLite;
using Xamarin.Forms;

namespace ReadBooks.ViewModels
{
    public class BooksVM : IPageLifecycleAware
    {
        public ICommand NewBookCommand { get; set; }
        public ICommand BookDetailsCommand { get; set; }
        public ObservableCollection<Item> SavedBooks { get; set; }

        INavigationService _navigationService;
        public BooksVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NewBookCommand = new DelegateCommand(NewBookAction);
            BookDetailsCommand = new DelegateCommand<object>(GoToDetail, CanGoToDetails);
            SavedBooks = new ObservableCollection<Item>();
            ReadSavedBooks();
        }

        private bool CanGoToDetails(object arg)
        {
            return arg != null;
        }

        private async void GoToDetail(object obj)
        {
            var selectedBook = (obj as ListView).SelectedItem as Item;

            var parameter = new NavigationParameters();
            parameter.Add("selected_book", selectedBook);
            await _navigationService.NavigateAsync(nameof(BooksDetailsPage), parameter);
        }

        private void ReadSavedBooks()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<Item>();
                var books = conn.Table<Item>().ToList();

                SavedBooks.Clear();
                foreach (var book in books)
                {
                    SavedBooks.Add(book);
                }
            }
        }

        private async void NewBookAction()
        {
            await _navigationService.NavigateAsync("NewBookPage");
        }

        public void OnAppearing()
        {
            ReadSavedBooks();
        }

        public void OnDisappearing()
        {

        }
    }
}
