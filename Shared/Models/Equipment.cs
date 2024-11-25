using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EquipmentsSystem.Shared.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string? SerialNumber { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfParche { get; set; }
        public string? Status { get; set; }
        public int? Quantity { get; set; } = 1;
        public string? Notes { get; set; } = "";
        // Foreign key for the Model
        public int? ModelId { get; set; }

        // Navigation property to represent the many-to-one relationship
        public Model? Model { get; set; }

        // Foreign key for the Model
        public int? TypeId { get; set; }

        // Navigation property to represent the many-to-one relationship

        public Type? Type { get; set; }

        // Foreign key for the Model
        public int? LocationId { get; set; }
        // Navigation property to represent the many-to-one relationship
        public Location? Location { get; set; }

    }
}
