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
    /// Логика взаимодействия для AnimeListPage.xaml
    /// </summary>
    public partial class AnimeListPage : Page
    {
        IAnimeService _animeService = new AnimeService(new JsonAnimeStorage());

        public AnimeListPage()
        {
            InitializeComponent();
            RenewList();
        }
        async void RenewList()
        {
            AnimesDataGrid.Items.Clear();
            var items = await _animeService.GetAnimeAsync();
            foreach (var item in items)
            {
                AnimesDataGrid.Items.Add(item);
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Anime anime = new()
            {
                Id = Guid.NewGuid(),
                Title = TitleTextBox.Text,
                Genre = GenreTextBox.Text,
                Status = StatusTextBox.Text
            };
            await _animeService.AddAnimeAsync(anime);
            RenewList();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Anime? anime = AnimesDataGrid.SelectedItem as Anime;
            if(anime==null)
            {
                MessageBox.Show("Не выбран элемент", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            await _animeService.DeleteAnimeAsync(anime.Id);
            RenewList();
        }
    }
}
