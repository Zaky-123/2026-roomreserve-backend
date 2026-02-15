using System;
using System.ComponentModel.DataAnnotations;

namespace RoomReserve.Api.Models;

public class Room
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(10)]
    public string Code { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Range(1, 500)]
    public int Capacity { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Location { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public RoomStatus Status { get; set; }
    
    // Audit fields
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public enum RoomStatus
{
    Available = 0,
    UnderMaintenance = 1,
    Occupied = 2
}