using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mitra.Domain.Entity;

namespace Mitra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCategoryController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<EventCategory>> CreateEventCategory(EventCategory category)
        {
            return null;
        }

    }
}
