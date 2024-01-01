using Mitra.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Interface
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDTO>> GetAllItem();
    }
}
