using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomReserve.Api.Data;
using RoomReserve.Api.DTOs;
using RoomReserve.Api.Models;

namespace RoomReserve.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<RoomsController> _logger;

    public RoomsController(AppDbContext context, ILogger<RoomsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/rooms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomResponseDto>>> GetRooms(
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = _context.Rooms.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(r => 
                    r.Name.Contains(search) || 
                    r.Code.Contains(search) || 
                    r.Location.Contains(search));
            }

            var totalCount = await query.CountAsync();
            
            var rooms = await query
                .OrderBy(r => r.Code)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new RoomResponseDto
                {
                    Id = r.Id,
                    Code = r.Code,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    Location = r.Location,
                    Description = r.Description,
                    Status = r.Status.ToString(),
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();

            return Ok(new
            {
                items = rooms,
                totalCount,
                page,
                pageSize,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting rooms");
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/rooms/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RoomResponseDto>> GetRoom(int id)
    {
        try
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound($"Ruangan dengan ID {id} tidak ditemukan");
            }

            return new RoomResponseDto
            {
                Id = room.Id,
                Code = room.Code,
                Name = room.Name,
                Capacity = room.Capacity,
                Location = room.Location,
                Description = room.Description,
                Status = room.Status.ToString(),
                CreatedAt = room.CreatedAt
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting room {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // POST: api/rooms
    [HttpPost]
    public async Task<ActionResult<RoomResponseDto>> CreateRoom(CreateRoomDto createDto)
    {
        try
        {
            // Cek duplicate code
            var existingRoom = await _context.Rooms
                .FirstOrDefaultAsync(r => r.Code == createDto.Code);
            
            if (existingRoom != null)
            {
                return BadRequest($"Kode ruangan '{createDto.Code}' sudah digunakan");
            }

            var room = new Room
            {
                Code = createDto.Code,
                Name = createDto.Name,
                Capacity = createDto.Capacity,
                Location = createDto.Location,
                Description = createDto.Description,
                Status = RoomStatus.Available,
                CreatedAt = DateTime.UtcNow
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            var response = new RoomResponseDto
            {
                Id = room.Id,
                Code = room.Code,
                Name = room.Name,
                Capacity = room.Capacity,
                Location = room.Location,
                Description = room.Description,
                Status = room.Status.ToString(),
                CreatedAt = room.CreatedAt
            };

            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating room");
            return StatusCode(500, "Internal server error");
        }
    }

    // PUT: api/rooms/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoom(int id, UpdateRoomDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID di URL tidak match dengan ID di body");
        }

        try
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound($"Ruangan dengan ID {id} tidak ditemukan");
            }

            // Cek duplicate code
            var existingRoom = await _context.Rooms
                .FirstOrDefaultAsync(r => r.Code == updateDto.Code && r.Id != id);
            
            if (existingRoom != null)
            {
                return BadRequest($"Kode ruangan '{updateDto.Code}' sudah digunakan");
            }

            room.Code = updateDto.Code;
            room.Name = updateDto.Name;
            room.Capacity = updateDto.Capacity;
            room.Location = updateDto.Location;
            room.Description = updateDto.Description;
            room.Status = updateDto.Status;
            room.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating room {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // DELETE: api/rooms/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        try
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound($"Ruangan dengan ID {id} tidak ditemukan");
            }

            // Soft delete
            room.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting room {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}