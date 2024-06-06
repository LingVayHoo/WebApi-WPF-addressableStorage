using ADS.Models;
using ADS.Models.DataBaseManager;
using DataBase;
using DataBase.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ADS.Services
{
    public class DataManager
    {
        private IDataBase _dataBaseApiModel;
        public DataManager(DataBaseApiModel dataBaseApiModel)
        {
            _dataBaseApiModel = dataBaseApiModel;
        }

        public DataManager(PostgreSQLApiModel postgreSQLApiModel)
        {
            _dataBaseApiModel = postgreSQLApiModel;
        }

        public AddressDBModel addressDBModel
        {
            get
            {
                return new AddressDBModel();
            }
        }

        public ObservableCollection<AddressModel> GetAllData()
        {
            return Convert<AddressModel>(_dataBaseApiModel.GetAllContent());
        }

        public ObservableCollection<AddressModel> GetDataByArticle(string article)
        {
            return Convert<AddressModel>(_dataBaseApiModel.GetContentByArticle(article));
        }

        public string[] GetAllInfo(string article, bool isFindByArticle)
        {
            string[] allInfo = new string[5];
            string jsonString;
            if (isFindByArticle) jsonString = _dataBaseApiModel.GetDataFromMyWarehouseByArt(article).Result;
            else jsonString = _dataBaseApiModel.GetDataFromMyWarehouseByName(article).Result;

            dynamic? jsonObject = JsonConvert.DeserializeObject(jsonString);

            dynamic? artName;
            dynamic? artQty;
            try
            {
                artName = jsonObject?.rows[0].name;
                artQty = jsonObject?.rows[0].stock;
            }
            catch (Exception)
            {
                allInfo[0] = string.Empty;
                allInfo[1] = string.Empty;
                return allInfo;
            }            

            if (artName != null) allInfo[0] = artName;            
            if (artQty != null) allInfo[1] = artQty;

            return allInfo;
        }

        public async Task<bool> CreateRecord(AddressDBModel addressDBModel)
        {
            return await _dataBaseApiModel.PostAddressDBModel(addressDBModel);
        }

        public async Task<bool> EditRecord(AddressDBModel addressDBModel)
        {
            return await _dataBaseApiModel.PutAddressDBModel(addressDBModel.Id, addressDBModel);
        }

        public async Task<bool> DeleteRecord(AddressDBModel addressDBModel)
        {
            return await _dataBaseApiModel.DeleteAddressDBModel(addressDBModel.Id);
        }

        private ObservableCollection<T> Convert<T>(IEnumerable original)
        {
            List<AddressModel> converted = new List<AddressModel>();
            foreach (var e in original)
            {
                if (e != null) converted.Add(new AddressModel(e as AddressDBModel));
            }
            return new ObservableCollection<T>(converted.Cast<T>());
        }
    }
}
