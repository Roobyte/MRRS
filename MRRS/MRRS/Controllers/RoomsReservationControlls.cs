using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MRRS.Model;
using MRRS.Service;
namespace MRRS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsReservationControlls : ControllerBase        
    {
        private readonly RoomResService _roomResService;
        public RoomsReservationControlls(RoomResService roomResService)
        {
            _roomResService = roomResService;
        }

        [HttpGet("GetAllReservation")]
        public async Task<ActionResult<List<RoomReservation>>> GetAllReservation()
        {
            return await _roomResService.GetAll();
        }
        [HttpGet("GetReservationById{id}")]
        public async Task<ActionResult<RoomReservation?>> GetReservationById(Guid id)
        {
            try
            {
                var reservation = await _roomResService.GetById(id);
                return reservation;
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }    
        }
        [HttpGet("DailySchedule{roomId}")]
        public async Task<ActionResult<List<RoomReservation>>> GetReservationById(Guid roomId,
            [FromQuery] string date)
        {
            try
            {
                return await _roomResService.DailySchedule(roomId, date);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
            
        }
        [HttpPost("AddReservation")]
        public async Task<ActionResult> AddReservation
            (
             [FromQuery] DateTime beginT,
             [FromQuery] DateTime endT,
             [FromQuery] Guid roomId,
             [FromQuery] Guid userId
            )
        {        
            try
            {
                await _roomResService.Add(beginT, endT, roomId, userId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }
        [HttpPut("UpdateReservation{id}")]
        public async Task<IActionResult> UpdateReservation(Guid id,
             [FromQuery] DateTime beginT,
             [FromQuery] DateTime endT,
             [FromQuery] Guid roomId,
             [FromQuery] Guid userId)
        {
            try
            {
                _roomResService.Update(id, beginT, endT, roomId, userId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }
        [HttpDelete("DeleteReservation{id}")]
        public async Task<IActionResult> DeleteReservation(Guid id)
        {
            await _roomResService.Delete(id);
            return NoContent();
        }
    }
}
