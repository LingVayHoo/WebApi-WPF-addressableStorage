using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DataBase
{
    public interface IDataBase
    {
        public IEnumerable<AddressDBModel>? GetAllContent();
        public IEnumerable<AddressDBModel>? GetContentByArticle(string article);
        public Task<string> GetDataFromMyWarehouseByArt(string article);
        public Task<string> GetDataFromMyWarehouseByName(string article);
        public Task<bool> PutAddressDBModel(Guid id, AddressDBModel addressDBModel);
        public Task<bool> PostAddressDBModel(AddressDBModel addressDBModel);
        public Task<bool> DeleteAddressDBModel(Guid id);
    }
}
