using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentsSystem.Shared.Models
{
    public class UserChangePassword
    {
        public int Id { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
