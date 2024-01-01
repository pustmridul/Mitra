using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;

namespace Mitra.API.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

            protected ResponseDto _response;
            private IItemService _itemService;

            public ItemsController(IItemService itemService)
            {
                _itemService = itemService;
                this._response = new ResponseDto();
            }
            [HttpGet]
            public async Task<object> Get()
            {
                try
                {
                    IEnumerable<ItemDTO> items = await _itemService.GetAllItem();
                    _response.Result = items;
                }
                catch (Exception ex)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages
                         = new List<string>() { ex.ToString() };
                }
                return _response;
            }
        }
}
