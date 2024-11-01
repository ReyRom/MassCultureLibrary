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
        public AnimeListPage()
        {
            InitializeComponent();
            RenewList();
        }
        async void RenewList()
        {
            AnimeListView.Items.Clear();
            IAnimeService animeService = new AnimeService(new JsonAnimeStorage());
            var items = await animeService.GetAnimeAsync();
            foreach (var item in items)
            {
                AnimeListView.Items.Add(item);
            }
        }
    }
}
