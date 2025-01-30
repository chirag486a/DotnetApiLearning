using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers.QueryObj
{
    public class StockGetAllQueryObj
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public uint PageNumber { get; set; } = 1;
        public uint PageSize { get; set; } = 20;
    }
}