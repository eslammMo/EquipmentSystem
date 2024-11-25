﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentsSystem.Shared.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Equipment>? Equipment { get; set; }
    }
}