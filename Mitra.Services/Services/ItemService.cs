using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mitra.Domain;
using Mitra.Domain.Entity;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;

namespace Mitra.Services.Services
{
    public class ItemService : IItemService
    {
        private IMapper _mapper;
        private AppDbContext _db;
        public ItemService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _db = context;
        }
        public async Task<IEnumerable<ItemDTO>> GetAllItem()
        {
            List<Item> datalist = await _db.Items.ToListAsync();
            return _mapper.Map<List<ItemDTO>>(datalist);

        }
    }
}
