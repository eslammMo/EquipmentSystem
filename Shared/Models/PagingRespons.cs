using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentsSystem.Shared.Models
{
  public class PagingResponse<T>
        {
        public T? Data { get; set; }

        public int TotalRecords { get; set; }
        public int CurrentPageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;

        public PagingResponse(T data, int totalRecords, int currentPageNumber, int pageSize)
        {
            Data = data;
            TotalRecords = totalRecords;
            CurrentPageNumber = currentPageNumber;
            PageSize = pageSize;

            TotalPages = Convert.ToInt32(Math.Ceiling((double)TotalRecords / (double)pageSize));
            HasNextPage = CurrentPageNumber < TotalPages;
            HasPreviousPage = CurrentPageNumber > 1;
        }
   }
}
