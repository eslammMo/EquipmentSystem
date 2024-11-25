using EquipmentsSystem.Client.Services.EquipmentService;
using EquipmentsSystem.Shared.Models;
using Microsoft.VisualBasic;
using Syncfusion.Blazor.Data;
using System.Drawing.Printing;
using System.Globalization;
using System.Net.Http.Json;
using System.Xml.Linq;
using static MudBlazor.CategoryTypes;
using static System.Net.WebRequestMethods;
using Type = EquipmentsSystem.Shared.Models.Type;

namespace EquipmentsSystem.Client.Services.EquipmentService
{
    public class EquipmentService : IEquipmentService
    {
        private readonly HttpClient _http;

        public EquipmentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<List<Equipment>>> GetEquipments(
                                 string? Type = null
                                , string? Model = null, string? SN = null, string? Name = null
                                , string? DOPRange = null, string? Location = null
                                , string? Status = null, int? minQuantity = null, int? maxQuantity = null)
        {
            var result =
             await _http.GetFromJsonAsync<ServiceResponse<List<Equipment>>>($"api/Equipment?" +
             $"Type={Type}&" +
             $"Model={Model}&" +
             $"SN={SN}&" +
             $"Name={Name}&" +
             $"DOPRange={DOPRange}&" +
             $"Location={Location}&" +
             $"Status={Status}&" +
             $"minQuantity={minQuantity}&" +
             $"maxQuantity={maxQuantity}"
             );

            return result;
        }

        public async Task<PagingResponse<List<Equipment>>> GetEquipmentsPagination(
                                int currentPageNumber = 1, int pageSize = 10
                                , string? sortDirection = "ASC", string? sortBy = null, string? Type = null
                                , string? Model = null, string? SN = null, string? Name = null
                                , string? DOPRange = null, string? Location = null
                                , string? Status = null, int? minQuantity = null, int? maxQuantity = null)
        {
            var result =
               await _http.GetFromJsonAsync<PagingResponse<List<Equipment>>>($"api/Equipment/GetEquipmentsPagination" +
               $"?currentPageNumber={currentPageNumber}&" +
               $"pageSize={pageSize}&" +
               $"sortDirection={sortDirection}&" +
               $"sortBy={sortBy}&" +
               $"Type={Type}&" +
               $"Model={Model}&" +
               $"SN={SN}&" +
               $"Name={Name}&" +
               $"DOPRange={DOPRange}&" +
               $"Location={Location}&" +
               $"Status={Status}&" +
               $"minQuantity={minQuantity}&" +
               $"maxQuantity= {maxQuantity}"
               );

            return result;
        }


        public async Task<ServiceResponse<Equipment>> GetSingleEquipment(int id)
        {
            var result =
                await _http.GetFromJsonAsync<ServiceResponse<Equipment>>($"api/Equipment/{id}");
            return result;
        }
        public async Task<ServiceResponse<List<Type>>> GetTypes(string? name)
        {
            var result =
                await _http.GetFromJsonAsync<ServiceResponse<List<Type>>>($"api/Equipment/GetTypes?name={name}");
            return result;
        }
        public async Task<ServiceResponse<List<Model>>> GetModels(string? name)
        {
            var result =
                await _http.GetFromJsonAsync<ServiceResponse<List<Model>>>($"api/Equipment/GetModels?name={name}");
            return result;
        }        
        public async Task<ServiceResponse<List<string>>> GetStatus()
        {
            var result =
                await _http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/Equipment/GetStatus");
            return result;
        }
        public async Task<ServiceResponse<List<Location>>> GetLocations(string? name)
        {
            var result =
                await _http.GetFromJsonAsync<ServiceResponse<List<Location>>>($"api/Equipment/GetLocations");
            return result;
        }
        public async Task<ServiceResponse<Equipment>> AddEquipment(Equipment obj)
        {
            var result = await _http.PostAsJsonAsync("api/Equipment", obj);
            var newEquip = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<Equipment>>());
            return newEquip;
        }
        public async Task<ServiceResponse<Type>> AddType(Type obj)
        {
            var result = await _http.PostAsJsonAsync("api/Equipment/AddType", obj);
            var newEquip = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<Type>>());
            return newEquip;
        }
        public async Task<ServiceResponse<Model>> AddModel(Model obj)
        {
            var result = await _http.PostAsJsonAsync("api/Equipment/AddModel", obj);
            var newEquip = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<Model>>());
            return newEquip;
        }
        public async Task<ServiceResponse<Location>> AddLocation(Location obj)
        {
            var result = await _http.PostAsJsonAsync("api/Equipment/AddLocation", obj);
            var newEquip = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<Location>>());
            return newEquip;
        }
        public async Task<Equipment> UpdateEquipment(Equipment obj)
        {
            var result = await _http.PutAsJsonAsync($"api/Equipment", obj);
            return (await result.Content.ReadFromJsonAsync<ServiceResponse<Equipment>>()).Data;
        }
        public async Task<ServiceResponse<bool>> DelteEquipment(int id)
        {
            var result = await _http.DeleteAsync($"api/Equipment/{id}");
            return (await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>());
        }
    }
}
