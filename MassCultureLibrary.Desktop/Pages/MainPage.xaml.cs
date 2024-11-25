using MassCultureLibrary.Animes;
using System.Windows;
using System.Windows.Controls;

namespace MassCultureLibrary.Desktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly AnimeService _animeService = new(new JsonAnimeStorage());

        public MainPage()
        {
            InitializeComponent();
            UpdateAnimeList();
        }

        private async void UpdateAnimeList()
        {
            AnimeDataGrid.Items.Clear();
            var animes = await _animeService.GetAnimeAsync();
            foreach (var anime in animes)
                AnimeDataGrid.Items.Add(anime);
        }

        private async void AddGameButton_Click(object sender, RoutedEventArgs e)
        {
            Anime anime = new() { Id = Guid.NewGuid(), Title = AnimeNameTextBox.Text, Genre = AnimeGenreTextBox.Text, Status = AnimeStatusTextBox.Text };
            await _animeService.AddAnimeAsync(anime);
            UpdateAnimeList();
        }
    }
}
