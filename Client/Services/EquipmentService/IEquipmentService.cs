﻿using EquipmentsSystem.Shared.Models;
using Type = EquipmentsSystem.Shared.Models.Type;

namespace EquipmentsSystem.Client.Services.EquipmentService
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
        Task<ServiceResponse<List<Type>>> GetTypes(string? name=null);
        Task<ServiceResponse<List<Model>>> GetModels(string? name=null);
        Task<ServiceResponse<List<Location>>> GetLocations(string? name = null);
        Task<ServiceResponse<List<string>>> GetStatus();
        Task<ServiceResponse<Model>> AddModel(Model obj);
        Task<ServiceResponse<Location>> AddLocation(Location obj);
        Task<ServiceResponse<Type>> AddType(Type obj);
        Task<ServiceResponse<Equipment>> AddEquipment(Equipment obj);
        Task<Equipment> UpdateEquipment(Equipment obj);
        Task<ServiceResponse<bool>> DelteEquipment(int id);
    }
}