using EquipmentsSystem.Server.Services.EquipmentService;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using MudBlazor;
using Microsoft.EntityFrameworkCore;
using Type = EquipmentsSystem.Shared.Models.Type;

namespace EquipmentsSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _EquipmentService;
        public EquipmentController(IEquipmentService equipmentService)
        {
            _EquipmentService = equipmentService;
        }


        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Equipment>>>> GetEquipments(string? Type, string? Model, string? SN,
                                                        string? Name, string? DOPRange, string? Location, string? Status,
                                                        int? minQuantity = null, int? maxQuantity = null)
        {
            var result = await _EquipmentService.GetEquipments(
                Type: Type,
                Model: Model,
                SN: SN,
                Name: Name,
                DOPRange: DOPRange,
                Location: Location,
                Status: Status,
                minQuantity: minQuantity,
                maxQuantity: maxQuantity
                );
            return Ok(result);
        }

        [HttpGet("GetEquipmentsPagination")]
        public async Task<ActionResult<ServiceResponse<List<Equipment>>>> GetEquipmentsPagination(int currentPageNumber = 1, int pageSize = 10,
                                                                            string? sortDirection = "ASC", string? sortBy = null, string? Type = null
                                                                         , string? Model = null, string? SN = null, string? Name = null
                                                                            , string? DOPRange = null, string? Location = null
                                                                         , string? Status = null, int? minQuantity = null, int? maxQuantity = null)
        {
            var result = await _EquipmentService.GetEquipmentsPagination(
                currentPageNumber : currentPageNumber,
                pageSize : pageSize,
                //sortDirection = "ASC",
                //sortBy = null,
                Type: Type,
                Model: Model,
                SN: SN,
                Name: Name,
                DOPRange: DOPRange,
                Location: Location,
                Status: Status,
                minQuantity: minQuantity,
                maxQuantity: maxQuantity
                );
            return Ok(result);
        }
        [HttpGet("{EquipId}")]
        public async Task<ActionResult<ServiceResponse<Equipment>>> GetSingleEquipment(int id)
        {
            var result = _EquipmentService.GetSingleEquipment(id);
            return Ok(result);
        }

        [HttpGet("GetTypes")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetTypes(string? name)
        {
            var result = await _EquipmentService.GetTypes(name);
            return Ok(result);
        }

        [HttpGet("GetModels")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetModels(string? name)
        {
            var result = await _EquipmentService.GetModels(name);
            return Ok(result);
        }

        [HttpGet("GetLocations")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetLocations(string? name)
        {
            var result = await _EquipmentService.GetLocations(name);
            return Ok(result);
        }        
        [HttpGet("GetStatus")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetStatus()
        {
            var result = await _EquipmentService.GetStatus();
            return Ok(result);
        }

        [HttpPost]

        public async Task<ActionResult<ServiceResponse<Equipment>>> AddEquipment(Equipment obj)
        {
            var result = await _EquipmentService.AddEquipment(obj);

            return Ok(result);
        }

        [HttpPost("AddType")]

        public async Task<ActionResult<ServiceResponse<Type>>> AddType(Type obj)
        {
            var result = await _EquipmentService.AddType(obj);

            return Ok(result);
        }


        [HttpPost("AddModel")]

        public async Task<ActionResult<ServiceResponse<Model>>> AddModel(Model obj)
        {
            var result = await _EquipmentService.AddModel(obj);

            return Ok(result);
        }


        [HttpPost("AddLocation")]

        public async Task<ActionResult<ServiceResponse<Location>>> AddLocation(Location obj)
        {
            var result = await _EquipmentService.AddLocation(obj);

            return Ok(result);
        }


        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Equipment>>> UpdateEquipment(Equipment obj)
        {
            var result = await _EquipmentService.UpdateEquipment(obj);
            return Ok(result);
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult<ServiceResponse<bool>>> DelteEquipment(int id)
        {
            var result = await _EquipmentService.DelteEquipment(id);

            return Ok(result);
        }

    }
}
