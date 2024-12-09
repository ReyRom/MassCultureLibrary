using MassCultureLibrary.Animes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            LoadAnimeList();//При прееходе на страницу загрузит данные
        }

        //Метод для обновления DataGrid
        private async void LoadAnimeList()
        {
            AnimeDataGrid.Items.Clear();//очищаем 
            var animes = await _animeService.GetAnimeAsync();//получаем список аниме
            foreach (var anime in animes)
                AnimeDataGrid.Items.Add(anime); //загружаем в DataGrid
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var anime = AnimeDataGrid.SelectedItem as Anime;
            await _animeService.DeleteAnimeAsync(anime.Id);
            LoadAnimeList(); 
        }

        //Метод для добавления аниме в список
        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Anime anime = new()
            {
                Id = Guid.NewGuid(),
                Title = AnimeNameTextBox.Text,
                Genre = AnimeGenreTextBox.Text,
                Status = AnimeStatusTextBox.Text
            };
            await _animeService.AddAnimeAsync(anime); //Добавляем аниме в список
            LoadAnimeList(); //Загружаем список 
        }
    }
}
