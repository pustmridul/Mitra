using Mitra.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Common
{
    public class PaginatedResponse<T> : IPaginatedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
