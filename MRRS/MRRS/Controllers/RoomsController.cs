using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MRRS;
using MRRS.Model;
using MRRS.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MRRS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : Controller
    {
        private readonly RoomService _roomService;
        public RoomsController(RoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpGet("GetAllRooms")]
        public async Task<ActionResult<List<Room>>> GetAllRooms()
        {
            return await _roomService.GetAll();
        }
        [HttpGet("GetRoomsById{id}")]
        public async Task<ActionResult<Room?>> GetRoomsById(Guid id)
        {
            try
            {
                var reservation = await _roomService.GetById(id);
                return reservation;
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPost("CreateRoom")]
        public async Task<ActionResult> CreateRoom(
            [FromQuery] int capacity,
            [FromQuery] string name)
        {
            try
            {
                await _roomService.Create(capacity, name);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }
        [HttpPut("UpdateRoom{id}")]
        public async Task<ActionResult> UpdateRoom(Guid id,
             [FromQuery] int capacity,
             [FromQuery] string name)
        {
            try
            {
                await _roomService.Update(id, capacity,name);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }
        [HttpDelete("DeleteRoom{id}")]
        public async Task<ActionResult> DeleteRoom(Guid id)
        {
            await _roomService.Delete(id);
            return NoContent();
        }
    }
}
