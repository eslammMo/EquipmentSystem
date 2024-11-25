using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Syncfusion.Blazor.Data;
using System.Drawing.Printing;
using System.Globalization;
using static MudBlazor.CategoryTypes;
using Type = EquipmentsSystem.Shared.Models.Type;

namespace EquipmentsSystem.Server.Services.EquipmentService
{
    public class EquipmentService : IEquipmentService
    {
        private readonly DataContext _context;
        public EquipmentService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Equipment>>> GetEquipments(string? Type, string? Model, string? SN,
                                                        string? Name, string? DOPRange, string? Location, string? Status,
                                                        int? minQuantity = null, int? maxQuantity = null)
        {
            var dbEquips = await GetEquipmentsQuery(
                Type: Type, Model: Model, SN: SN, Name: Name, DOPRange: DOPRange,
                Location: Location, Status: Status, minQuantity: minQuantity, maxQuantity: maxQuantity
                );

            return new ServiceResponse<List<Equipment>>
            {
                Data = await dbEquips.OrderByDescending(e=>e.DateOfParche).ToListAsync()
            };
        }
        public async Task<PagingResponse<List<Equipment>>> GetEquipmentsPagination(int currentPageNumber = 1, int pageSize = 10,
                                                                            string? sortDirection = "ASC", string? sortBy = null, string? Type = null
                                                                         , string? Model = null, string? SN = null, string? Name = null
                                                                            , string? DOPRange = null, string? Location = null
                                                                         , string? Status = null, int? minQuantity = null, int? maxQuantity = null)
        {
            var dbEquips = await GetEquipmentsQuery(
                Type: Type, Model: Model, SN: SN, Name: Name, DOPRange: DOPRange,
                Location: Location, Status: Status, minQuantity: minQuantity, maxQuantity: maxQuantity
                );
            int maxPageSize = 1000;
            pageSize = (pageSize > 0 && pageSize <= maxPageSize) ? pageSize : pageSize;
            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            // else if (sortBy == "RecruitingDate")
            //{
            //    if (sortDirection == "ASC")
            //    {
            //        dbSoldiers = dbSoldiers.OrderBy(r => r.RecruitingDate);
            //    }
            //    else
            //    {
            //        dbSoldiers = dbSoldiers.OrderByDescending(r => r.RecruitingDate);

            //    }
            // }
            var count = dbEquips.Count();
            var Equips = await dbEquips.OrderByDescending(e=>e.Id).Skip(skip).Take(take).ToListAsync();
            return new PagingResponse<List<Equipment>>(Equips, count, currentPageNumber, pageSize);

        }
        public async Task<IQueryable<Equipment>> GetEquipmentsQuery(string? Type, string? Model, string? SN, string? Name,
                                                                        string? DOPRange, string? Location, string? Status
                                                                            , int? minQuantity, int? maxQuantity)
        {
            var count = _context.Equipments.Count();
            var dbEquips = _context.Equipments.Include(s=>s.Location).Include(s=>s.Type).Include(s=>s.Model).AsQueryable();
            if (!string.IsNullOrEmpty(Type))
            {
                var typeList = Type.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();
                dbEquips = dbEquips.Where(e => typeList.Contains(e.Type.Name));
            }

            if (!string.IsNullOrEmpty(Model))
            {
                var modelList = Model.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(m => m.Trim()).ToList();
                dbEquips = dbEquips.Where(e => modelList.Contains(e.Model.Name));
            }

            if (!string.IsNullOrEmpty(Location))
            {
                var locationList = Location.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(l => l.Trim()).ToList();
                dbEquips = dbEquips.Where(e => locationList.Contains(e.Location.Name));
            }

            dbEquips = dbEquips.Where(e => string.IsNullOrEmpty(SN) || e.SerialNumber.Contains(SN));
            dbEquips = dbEquips.Where(e => string.IsNullOrEmpty(Name) || e.Name.Contains(Name));
            if (DOPRange != null)
            {
                List<string> dates = DOPRange.Split("-").ToList();
                DateTime DateFrom = DateTime.ParseExact(dates[0], "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                DateTime DateTo = DateTime.ParseExact(dates[1], "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                dbEquips = dbEquips.Where(e => e.DateOfParche >= DateFrom && e.DateOfParche <= DateTo);
            }
            dbEquips = dbEquips.Where(e => string.IsNullOrEmpty(Status) || e.Status.Contains(Status));

            dbEquips = dbEquips.Where(e => minQuantity == null || minQuantity == 0 || e.Quantity >= minQuantity);
            dbEquips = dbEquips.Where(e => maxQuantity == null || maxQuantity == 0 || e.Quantity <= maxQuantity);

            return dbEquips;
        }

        public async Task<ServiceResponse<Equipment>> GetSingleEquipment(int id)
        {
            var response = new ServiceResponse<Equipment>();
            var obj = await 
             _context.Equipments.AsSplitQuery().FirstOrDefaultAsync(s => s.Id == id);

            if (obj == null)
            {
                response.Success = false;
                response.Message = "Sorry, This Equipment does not exist.";
            }
            else
            {
                response.Data = obj;
            }
            return response;
        }
        public async Task<ServiceResponse<List<Type>>> GetTypes(string? name)
        {
            var  types = await _context.Types.Where(t=> string.IsNullOrEmpty(name) || t.Name.Contains(name)).OrderByDescending(t=>t.Name).ToListAsync();
            var response = new ServiceResponse<List<Type>>();
            if (types == null || types.Count <= 0)
            {
                response.Success = false;
                response.Message = "There is No Types added yet";
                response.Data = new List<Type>();
                return response;
            }
            else
            {
                response.Data = types;
                return response;
            }
        }
        public async Task<ServiceResponse<List<Model>>> GetModels(string? name)
        {
            var Models = await _context.Models.Where(t => string.IsNullOrEmpty(name) || t.Name.Contains(name)).OrderByDescending(t => t.Name).ToListAsync();
            var response = new ServiceResponse<List<Model>>();

            if (Models == null || Models.Count <= 0)
            {
                response.Success = false;
                response.Message = "There is No Models added yet";
                response.Data = new List<Model>();
                return response;
            }
            else
            {
                response.Data = Models;
                return response;
            }
        }
        public async Task<ServiceResponse<List<string>>> GetStatus()
        {
            var Status = await _context.Equipments.Select(s => s.Status).Distinct().ToListAsync();
            var response = new ServiceResponse<List<string>>();

            if (Status == null || Status.Count <= 0)
            {
                response.Success = false;
                response.Message = "There is No Status added yet";
                response.Data = new List<string>();
                return response;
            }
            else
            {
                response.Data = Status;
                return response;
            }
        }
        public async Task<ServiceResponse<List<Location>>> GetLocations(string? name)
        {
            var Locations = await _context.Locations.Where(t => string.IsNullOrEmpty(name) || t.Name.Contains(name)).OrderByDescending(t => t.Name).ToListAsync();
            var response = new ServiceResponse<List<Location>>();

            if (Locations == null || Locations.Count <= 0)
            {
                response.Success = false;
                response.Message = "There is No Locations added yet";
                response.Data = new List<Location>();
                return response;
            }
            else
            {
                response.Data = Locations;
                return response;
            }
        }
        public async Task<ServiceResponse<Equipment>> AddEquipment(Equipment obj)
        {
            _context.Equipments.Add(obj);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Equipment>
            {
                Data = obj
            };
        }
        public async Task<ServiceResponse<Type>> AddType(Type obj)
        {
            if (await _context.Types.Where(t => t.Name == obj.Name).FirstOrDefaultAsync() != null)
                return new ServiceResponse<Type> { Success = false, Message = "Duplicated" };
            _context.Types.Add(obj);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Type>
            {
                Data = obj
            };
        }
        public async Task<ServiceResponse<Model>> AddModel(Model obj)
        {
            if (await _context.Models.Where(t => t.Name == obj.Name).FirstOrDefaultAsync() != null)
                return new ServiceResponse<Model> { Success = false, Message = "Duplicated" };
            _context.Models.Add(obj);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Model>
            {
                Data = obj
            };
        }
        public async Task<ServiceResponse<Location>> AddLocation(Location obj)
        {
            if (await _context.Locations.Where(t => t.Name == obj.Name).FirstOrDefaultAsync() != null)
                return new ServiceResponse<Location> { Success = false, Message = "Duplicated" };
            _context.Locations.Add(obj);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Location>
            {
                Data = obj
            };
        }
        public async Task<ServiceResponse<Equipment>> UpdateEquipment(Equipment obj)
        {
            var dbEquip= await _context.Equipments.FindAsync(obj.Id);
            if (dbEquip == null)
            {
                return new ServiceResponse<Equipment>
                {
                    Success = false,
                    Message = "Equipment not found",
                };
            }
            var EquipProperties = typeof(Equipment).GetProperties().Where(p => !p.PropertyType.IsClass || p.PropertyType == typeof(string)); // Filter out navigation properties and complex types
            foreach (var property in EquipProperties)
            {
                property.SetValue(dbEquip, property.GetValue(obj));
            }

            await _context.SaveChangesAsync();
            return new ServiceResponse<Equipment>
            {
                Data = obj,
            };
        }
        public async Task<ServiceResponse<bool>> DelteEquipment(int id)
        {
            var dbEquip = await _context.Equipments.FindAsync(id);
            if (dbEquip == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Equipment not found",
                    Data = false
                };
            }
            _context.Equipments.Remove(dbEquip);
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

    }
}
