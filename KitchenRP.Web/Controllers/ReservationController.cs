using System.Threading.Tasks;
using KitchenRP.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace KitchenRP.Web.Controllers
{
    [ApiController]
    [Route("reservation")]
    public class ReservationController: ControllerBase
    {


        [HttpPost]
        public Task<IActionResult> AddReservation(AddReservationRequest model)
        {

        }

    }
}
