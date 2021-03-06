using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KitchenRP.Domain.Commands;
using KitchenRP.Domain.Models;
using KitchenRP.Domain.Services;
using KitchenRP.Web.Models;
using Microsoft.AspNetCore.Mvc;
using NodaTime;

namespace KitchenRP.Web.Controllers
{
    [ApiController]
    [Route("reservation")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation(AddReservationRequest model)
        {
            var resource = await _reservationService.AddNewReservation(_mapper.Map<AddReservationCommand>(model));
            var uri = $"reservation/{resource.Id}";
            return Created(uri, new AddReservationResponse
            {
                Id = resource.Id,
                Uri = uri,
            });
        }

        [HttpGet]
        public async Task<IActionResult> QueryReservation([FromQuery] QueryReservationRequest model)
        {
            var reservations = await _reservationService.QueryReservations(_mapper.Map<QueryReservationCommand>(model));
            var mapped = reservations.Select(_mapper.Map<ReservationResponse>);
            return Ok(mapped);
        }

        [HttpPut]
        [Route("{id}/accept")]
        public async Task<IActionResult> AcceptReservation(StatusChangeRequest model)
        {
            var reservation = await _reservationService.AcceptReservation(new AcceptReservationCommand { Id = model.Id, UserId = model.UserId });
            return NoContent();
        }

        [HttpPut]
        [Route("{id}/deny")]
        public async Task<IActionResult> DenyReservation(StatusChangeRequest model)
        {
            var reservation = await _reservationService.DenyReservation(new DenyReservationCommand { Id = model.Id, UserId = model.UserId });
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteReservation(long id)
        {
            await _reservationService.DeleteReservation(new DeleteReservationCommand { Id = id });
            return NoContent();
        }
    }
}