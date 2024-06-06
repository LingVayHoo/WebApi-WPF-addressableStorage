using ADS.Models;
using ADS.PostgreSQL;
using ADS.Services;
using ADS.View;
using ADS.ViewModel;
using DataBase.Models;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataManager _dataManager;
        private AddressViewModel _addressViewModel;
        public MainWindow()
        {
            InitializeComponent();
            Preparing();
        }

        private void Preparing()
        {
            _dataManager = new(new Models.DataBaseManager.DataBaseApiModel());
            _addressViewModel = new(_dataManager);
            //AddressDBModel addressDBModel = new AddressDBModel()
            //{
            //    Article = "603.706.25",
            //    Zone = 13,
            //    Row = 1,
            //    Place = 2,
            //    Level = 10,
            //    Qty = 10
            //};
            //Postgre_SQL m = new();
            //m.PostAddressDBModel(addressDBModel);
            DataContext = _addressViewModel;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            TooltipFiled.Text = string.Empty;
            //Попытка поиска без изменения данных по артикулу и имени
            if (Search(ArticleField.Text, true)) return;
            if (Search(ArticleField.Text, false)) return;

            //Попытка поиска с измененными данными по артикулу и имени
            ArticleField.Text = ArticleField.Text.Replace(".", null);
            if (Search(ArticleField.Text, true)) return;

            ArticleField.Text = ArticleField.Text.Replace(".", null);
            if (Search(ArticleField.Text, false)) return;
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            AddressModel addressModel = MainDataGrid.SelectedItem as AddressModel;
            if (addressModel != null)
            {
                Console.WriteLine(addressModel.Id);
                var editRecord = Convert(addressModel);

                AddNew addNew = new AddNew(_addressViewModel, editRecord, false);
                addNew.ShowDialog();

                if (addNew.DialogResult == true)
                {
                    TooltipFiled.Text = "Успешно сохранено!";
                }
            }
            
        }

        private async void DeleteClick(object sender, RoutedEventArgs e)
        {
            AddressModel addressModel = MainDataGrid.SelectedItem as AddressModel;
            if(addressModel != null)
            {
                var deleteRecord = Convert(addressModel);
                await _addressViewModel.DeleteRecord(deleteRecord);
                TooltipFiled.Text = "Успешно удалено!";
            }            
        }

        private bool Search(string art, bool isFindByArticle)
        {
            _addressViewModel.UpdateArticle(art);
            ArtNameField.Text = _addressViewModel.GetAllInfo(art, isFindByArticle)[0];
            QtyField.Text = _addressViewModel.GetAllInfo(art, isFindByArticle)[1];
            return !(ArtNameField.Text == string.Empty);
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {    
            if (ArticleField.Text == string.Empty)
            {
                TooltipFiled.Text = string.Empty;
                return;
            }
            var newRecord = _addressViewModel.addressDBModel;
            newRecord.Article = ArticleField.Text;

            AddNew addNew = new AddNew(_addressViewModel, newRecord, true);
            addNew.ShowDialog();

            if (addNew.DialogResult == true)
            {
                TooltipFiled.Text = "Успешно сохранено!";
            }
        }

        private AddressDBModel Convert(AddressModel addressModel)
        {
            return new AddressDBModel
            {
                Id = addressModel.Id,
                Article = addressModel.Article,
                Zone = addressModel.Zone,
                Row = addressModel.Row,
                Place = addressModel.Place,
                Level = addressModel.Level,
                Qty = addressModel.Qty
            };
        }
    }
}