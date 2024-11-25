using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EquipmentsSystem.Shared.Models
{
    public class ReportItems
    {

        public string? ReportName { get; set; }
        public List<string>? items { get; set; } = new List<string>();
        public int Count { get; set; }
        public string? DataTableName { get; set; }
        public Dictionary<string, string>? parameters { get; set; } = new Dictionary<string, string>();

    }
    public class ReportItemsMultiple
    {

        public string? ReportName { get; set; }
        public List<string>? items { get; set; } = new List<string>();
        public List<int> Count { get; set; } = new List<int>();
        public List<int> RowsCount { get; set; } = new List<int>();
        public List<string>? DataTableName { get; set; } = new List<string>();
        public Dictionary<string, string>? parameters { get; set; } = new Dictionary<string, string>();

    }
}