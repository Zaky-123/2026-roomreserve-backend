using System;
using System.ComponentModel.DataAnnotations;

namespace RoomReserve.Api.Models;

public class BookingHistory
{
    public int Id { get; set; }
    
    [Required]
    public int BookingId { get; set; }
    
    public string? OldStatus { get; set; }
    
    [Required]
    public string NewStatus { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    public DateTime ChangedAt { get; set; }
    
    [StringLength(100)]
    public string? ChangedBy { get; set; }
    
    // Navigation property
    public Booking? Booking { get; set; }
}