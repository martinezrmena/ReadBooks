using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using Newtonsoft.Json;
using Prism.Commands;
using ReadBooks.Models;
using ReadBooks.ViewModels.Helpers;
using SQLite;

namespace ReadBooks.ViewModels
{
    public class NewBookVM
    {
        public ObservableCollection<Item> SearchResults { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public NewBookVM()
        {
            SearchResults = new ObservableCollection<Item>();
            SearchCommand = new DelegateCommand<string>(GetSearchResults);
            SaveCommand = new DelegateCommand<Item>(SaveBook, CanSaveBook);
        }

        private async void GetSearchResults(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetStringAsync($"https://www.googleapis.com/books/v1/volumes?q={query}&key={Constants.GOOGLE_BOOKS_API_KEY}");

                var data = JsonConvert.DeserializeObject<BooksAPI>(result);

                SearchResults.Clear();
                foreach (var book in data.items)
                {
                    SearchResults.Add(book);
                }
            }
        }

        private void SaveBook(Item book)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<Item>();
                int booksInserted = conn.Insert(book);
                if (booksInserted >= 1)
                {
                    App.Current.MainPage.DisplayAlert("Success", "Book saved", "Ok");
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Failure", "An error ocurred while saving the book, please try again.", "Ok");
                }
            }
        }

        private bool CanSaveBook(Item book)
        {
            return book != null;
        }
    }
}
