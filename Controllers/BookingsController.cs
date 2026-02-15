using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomReserve.Api.Data;
using RoomReserve.Api.DTOs;
using RoomReserve.Api.Models;

namespace RoomReserve.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<BookingsController> _logger;

    public BookingsController(AppDbContext context, ILogger<BookingsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/bookings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingResponseDto>>> GetBookings(
        [FromQuery] int? roomId,
        [FromQuery] string? status,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = _context.Bookings
                .Include(b => b.Room)
                .Where(b => b.DeletedAt == null)
                .AsQueryable();

            // Filter by room
            if (roomId.HasValue)
            {
                query = query.Where(b => b.RoomId == roomId);
            }

            // Filter by status
            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<BookingStatus>(status, true, out var statusEnum))
            {
                query = query.Where(b => b.Status == statusEnum);
            }

            // Filter by date range
            if (startDate.HasValue)
            {
                query = query.Where(b => b.StartTime >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                query = query.Where(b => b.EndTime <= endDate.Value);
            }

            // Search by borrower name/email/purpose
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(b => 
                    b.BorrowerName.Contains(search) ||
                    b.BorrowerEmail.Contains(search) ||
                    (b.Purpose != null && b.Purpose.Contains(search)));
            }

            var totalCount = await query.CountAsync();
            
            var bookings = await query
                .OrderByDescending(b => b.StartTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BookingResponseDto
                {
                    Id = b.Id,
                    RoomId = b.RoomId,
                    RoomName = b.Room != null ? b.Room.Name : "",
                    RoomCode = b.Room != null ? b.Room.Code : "",
                    BorrowerName = b.BorrowerName,
                    BorrowerEmail = b.BorrowerEmail,
                    BorrowerPhone = b.BorrowerPhone,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime,
                    Purpose = b.Purpose,
                    Status = b.Status.ToString(),
                    CreatedAt = b.CreatedAt
                })
                .ToListAsync();

            return Ok(new
            {
                items = bookings,
                totalCount,
                page,
                pageSize,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting bookings");
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/bookings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookingResponseDto>> GetBooking(int id)
    {
        try
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.Id == id && b.DeletedAt == null);

            if (booking == null)
            {
                return NotFound($"Peminjaman dengan ID {id} tidak ditemukan");
            }

            return new BookingResponseDto
            {
                Id = booking.Id,
                RoomId = booking.RoomId,
                RoomName = booking.Room?.Name ?? "",
                RoomCode = booking.Room?.Code ?? "",
                BorrowerName = booking.BorrowerName,
                BorrowerEmail = booking.BorrowerEmail,
                BorrowerPhone = booking.BorrowerPhone,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                Purpose = booking.Purpose,
                Status = booking.Status.ToString(),
                CreatedAt = booking.CreatedAt
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting booking {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // POST: api/bookings
    [HttpPost]
    public async Task<ActionResult<BookingResponseDto>> CreateBooking(CreateBookingDto createDto)
    {
        try
        {
            // Validasi room exists
            var room = await _context.Rooms.FindAsync(createDto.RoomId);
            if (room == null || room.DeletedAt != null)
            {
                return BadRequest($"Ruangan dengan ID {createDto.RoomId} tidak ditemukan");
            }

            // Validasi waktu
            if (createDto.StartTime >= createDto.EndTime)
            {
                return BadRequest("Waktu mulai harus lebih awal dari waktu selesai");
            }

            if (createDto.StartTime < DateTime.UtcNow)
            {
                return BadRequest("Waktu mulai tidak boleh di masa lalu");
            }

            // Cek bentrok booking
            var conflict = await _context.Bookings
                .AnyAsync(b => b.RoomId == createDto.RoomId &&
                              b.DeletedAt == null &&
                              b.Status != BookingStatus.Rejected &&
                              b.Status != BookingStatus.Cancelled &&
                              ((b.StartTime <= createDto.StartTime && b.EndTime > createDto.StartTime) ||
                               (b.StartTime < createDto.EndTime && b.EndTime >= createDto.EndTime) ||
                               (b.StartTime >= createDto.StartTime && b.EndTime <= createDto.EndTime)));

            if (conflict)
            {
                return BadRequest("Ruangan sudah dipesan pada waktu tersebut");
            }

            var booking = new Booking
            {
                RoomId = createDto.RoomId,
                BorrowerName = createDto.BorrowerName,
                BorrowerEmail = createDto.BorrowerEmail,
                BorrowerPhone = createDto.BorrowerPhone,
                StartTime = createDto.StartTime,
                EndTime = createDto.EndTime,
                Purpose = createDto.Purpose,
                Status = BookingStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Load room data for response
            await _context.Entry(booking).Reference(b => b.Room).LoadAsync();

            var response = new BookingResponseDto
            {
                Id = booking.Id,
                RoomId = booking.RoomId,
                RoomName = booking.Room?.Name ?? "",
                RoomCode = booking.Room?.Code ?? "",
                BorrowerName = booking.BorrowerName,
                BorrowerEmail = booking.BorrowerEmail,
                BorrowerPhone = booking.BorrowerPhone,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                Purpose = booking.Purpose,
                Status = booking.Status.ToString(),
                CreatedAt = booking.CreatedAt
            };

            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating booking");
            return StatusCode(500, "Internal server error");
        }
    }

    // PUT: api/bookings/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBooking(int id, UpdateBookingDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID di URL tidak match dengan ID di body");
        }

        try
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.Id == id && b.DeletedAt == null);

            if (booking == null)
            {
                return NotFound($"Peminjaman dengan ID {id} tidak ditemukan");
            }

            // Hanya bisa update jika masih Pending
            if (booking.Status != BookingStatus.Pending)
            {
                return BadRequest($"Tidak dapat mengubah peminjaman dengan status {booking.Status}");
            }

            // Validasi room exists
            var room = await _context.Rooms.FindAsync(updateDto.RoomId);
            if (room == null || room.DeletedAt != null)
            {
                return BadRequest($"Ruangan dengan ID {updateDto.RoomId} tidak ditemukan");
            }

            // Validasi waktu
            if (updateDto.StartTime >= updateDto.EndTime)
            {
                return BadRequest("Waktu mulai harus lebih awal dari waktu selesai");
            }

            // Cek bentrok booking (kecuali dirinya sendiri)
            var conflict = await _context.Bookings
                .AnyAsync(b => b.Id != id &&
                              b.RoomId == updateDto.RoomId &&
                              b.DeletedAt == null &&
                              b.Status != BookingStatus.Rejected &&
                              b.Status != BookingStatus.Cancelled &&
                              ((b.StartTime <= updateDto.StartTime && b.EndTime > updateDto.StartTime) ||
                               (b.StartTime < updateDto.EndTime && b.EndTime >= updateDto.EndTime) ||
                               (b.StartTime >= updateDto.StartTime && b.EndTime <= updateDto.EndTime)));

            if (conflict)
            {
                return BadRequest("Ruangan sudah dipesan pada waktu tersebut");
            }

            booking.RoomId = updateDto.RoomId;
            booking.BorrowerName = updateDto.BorrowerName;
            booking.BorrowerEmail = updateDto.BorrowerEmail;
            booking.BorrowerPhone = updateDto.BorrowerPhone;
            booking.StartTime = updateDto.StartTime;
            booking.EndTime = updateDto.EndTime;
            booking.Purpose = updateDto.Purpose;
            booking.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating booking {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // DELETE: api/bookings/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        try
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.Id == id && b.DeletedAt == null);

            if (booking == null)
            {
                return NotFound($"Peminjaman dengan ID {id} tidak ditemukan");
            }

            // Hanya bisa delete jika masih Pending
            if (booking.Status != BookingStatus.Pending)
            {
                return BadRequest($"Tidak dapat menghapus peminjaman dengan status {booking.Status}");
            }

            booking.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting booking {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}