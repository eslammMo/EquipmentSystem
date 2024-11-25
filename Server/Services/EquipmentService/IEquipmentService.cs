using EquipmentsSystem.Shared.Models;
using Type = EquipmentsSystem.Shared.Models.Type;

namespace EquipmentsSystem.Server.Services.EquipmentService
{
    public interface IEquipmentService
    {
        Task<ServiceResponse<List<Equipment>>> GetEquipments(string? Type, string? Model, string? SN,
                                                            string? Name, string? DOPRange, string? Location, string? Status, 
                                                            int? minQuantity = null, int? maxQuantity = null);
        Task<PagingResponse<List<Equipment>>> GetEquipmentsPagination(int currentPageNumber = 1, int pageSize = 10,
                                                                            string? sortDirection = "ASC", string? sortBy = null, string? Type = null
                                                                         , string? Model = null, string? SN = null, string? Name = null
                                                                            , string? DOPRange = null, string? Location = null
                                                                         , string? Status = null, int? minQuantity = null, int? maxQuantity = null);
        Task<ServiceResponse<Equipment>> GetSingleEquipment(int id);
        Task<ServiceResponse<List<Type>>> GetTypes(string? name);
        Task<ServiceResponse<List<Model>>> GetModels(string? name );
        Task<ServiceResponse<List<Location>>> GetLocations(string? name);
        Task<ServiceResponse<List<string>>> GetStatus();
        Task<ServiceResponse<Equipment>> AddEquipment(Equipment obj);
        Task<ServiceResponse<Equipment>> UpdateEquipment(Equipment obj);
        Task<ServiceResponse<bool>> DelteEquipment(int id);
        Task<ServiceResponse<Location>> AddLocation(Location obj);
        Task<ServiceResponse<Type>> AddType(Type obj);
        Task<ServiceResponse<Model>> AddModel(Model obj);
    }
}