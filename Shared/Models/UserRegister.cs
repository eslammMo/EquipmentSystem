using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentsSystem.Shared.Models
{
    public class UserRegister
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [Compare("Password",ErrorMessage ="كلمة السر غير مطابقه")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;

    }
}
