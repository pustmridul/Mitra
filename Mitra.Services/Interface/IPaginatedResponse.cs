using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Interface
{
    public interface IPaginatedResponse<T>
    {
        IEnumerable<T> Data { get; set; }
        int TotalRecords { get; set; }
    }

}
