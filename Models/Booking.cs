using System;
using System.ComponentModel.DataAnnotations;

namespace RoomReserve.Api.Models;

public class Booking
{
    public int Id { get; set; }
    
    [Required]
    public int RoomId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string BorrowerName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string BorrowerEmail { get; set; } = string.Empty;
    
    [Required]
    [StringLength(20)]
    public string BorrowerPhone { get; set; } = string.Empty;
    
    [Required]
    public DateTime StartTime { get; set; }
    
    [Required]
    public DateTime EndTime { get; set; }
    
    public string? Purpose { get; set; }
    
    public BookingStatus Status { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    // Navigation property
    public Room? Room { get; set; }
}

public enum BookingStatus
{
    Pending,
    Approved,
    Rejected,
    Cancelled,
    Completed
}