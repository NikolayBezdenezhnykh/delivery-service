using Api.Dtos;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/v{version:apiVersion}/delivery/reserve")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class DeliveryController : ControllerBase
    {
        private readonly DeliveryDbContext _dbContext;

        public DeliveryController(DeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Reserve([FromBody] DeliveryDto delivery)
        {
            var availableCourier = await _dbContext.AvailableCouriers.SingleOrDefaultAsync(d => delivery.Date.Date == d.Date.Value.Date);
            if (availableCourier == null
               || availableCourier.Count <= 0)
            {
                return NotFound($"Нет свободных курьеров на дату: '{delivery.Date:dd.MM.yyyy}'.");
            }

            availableCourier.Count--;

            var reserveCourier = new ReserveCourier()
            {
                DateReserve = DateTime.UtcNow,
                Status = (int)ReserveCourierStatus.Approve,
                Date = delivery.Date,
                Address = delivery.Address,
            };

            _dbContext.ReserveCouriers.Add(reserveCourier);
            await _dbContext.SaveChangesAsync();

            return Ok(new { reserveCourier.Id });
        }

        [HttpPut("cancelled/{reserveId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Cancelled(int reserveId)
        {
            var reservedCourier = await _dbContext.ReserveCouriers.SingleOrDefaultAsync(pr => pr.Id == reserveId);
            if (reservedCourier == null) return NotFound();

            var availableCourier = await _dbContext.AvailableCouriers.SingleAsync(pr => pr.Date.Value.Date == reservedCourier.Date.Value.Date);
            
            availableCourier.Count++;
            reservedCourier.Status = (int)ReserveCourierStatus.Cancelled;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}