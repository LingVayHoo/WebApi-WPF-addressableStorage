using ADS.Services;
using ADS.ViewModel;
using DataBase.Models;
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
using System.Windows.Shapes;

namespace ADS.View
{
    /// <summary>
    /// Логика взаимодействия для AddNew.xaml
    /// </summary>
    public partial class AddNew : Window
    {
        public AddNew()
        {
            InitializeComponent();
        }

        public AddNew(AddressViewModel addressViewModel, AddressDBModel addressDBModel, bool isNew)
        {
            InitializeComponent();

            ArticleFiled.Text = addressDBModel.Article;
            ZoneFiled.Text = addressDBModel.Zone;
            RowField.Text = addressDBModel.Row;
            PlaceField.Text = addressDBModel.Place;
            LevelField.Text = addressDBModel.Level;
            QtyField.Text = addressDBModel.Qty;           

            CancelButton.Click += delegate
            {
                this.DialogResult = false;
            };

            OkButton.Click += async delegate
            {
                addressDBModel.Article = ArticleFiled.Text;
                addressDBModel.Zone = ZoneFiled.Text;
                addressDBModel.Row = RowField.Text;
                addressDBModel.Place = PlaceField.Text;
                addressDBModel.Level = LevelField.Text;
                addressDBModel.Qty = QtyField.Text;
                if (isNew) await addressViewModel.CreateRecord(addressDBModel);
                else await addressViewModel.EditRecord(addressDBModel);
                this.DialogResult = true;
            };
        }
    }
}
