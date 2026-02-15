using System.ComponentModel.DataAnnotations;

namespace RoomReserve.Api.DTOs;

public class UpdateStatusDto
{
    [Required(ErrorMessage = "Status baru wajib diisi")]
    public string NewStatus { get; set; } = string.Empty;
    
    [StringLength(500, ErrorMessage = "Catatan maksimal 500 karakter")]
    public string? Notes { get; set; }
}

public class BookingHistoryDto
{
    public int Id { get; set; }
    public string? OldStatus { get; set; }
    public string NewStatus { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime ChangedAt { get; set; }
    public string? ChangedBy { get; set; }
}

public class StatusUpdateResponseDto
{
    public string Message { get; set; } = string.Empty;
    public string NewStatus { get; set; } = string.Empty;
}