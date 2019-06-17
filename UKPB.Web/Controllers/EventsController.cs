using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UKPBS.Services;
using UKPBS.Services.Requests;

namespace UKPBS.Web.Controllers
{
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        [HttpGet("from/{startDate:datetime}/to/{endDate:datetime}")]
        public async Task<ActionResult> GetList(EventRequest request)
        {
            var result = await _eventService.GetEventsAsync(request);
            if (result.Success)
                return Ok(result.Payload);
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}/{startDate:datetime}")]
        public async Task<ActionResult> GetEventDetail(int id, DateTime startDate)
        {
            var result = await _eventService.GetEventDetailAsync(id, startDate);
            if (result.Success)
                return Ok(result.Payload);
            return BadRequest(result.ErrorMessage);
        }
    }
}
