using System.ComponentModel.DataAnnotations;
using RoomReserve.Api.Models;

namespace RoomReserve.Api.DTOs;

public class CreateRoomDto
{
    [Required(ErrorMessage = "Kode ruangan wajib diisi")]
    [StringLength(10, MinimumLength = 2, ErrorMessage = "Kode ruangan harus antara 2-10 karakter")]
    public string Code { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Nama ruangan wajib diisi")]
    [StringLength(100, ErrorMessage = "Nama ruangan maksimal 100 karakter")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Kapasitas wajib diisi")]
    [Range(1, 500, ErrorMessage = "Kapasitas harus antara 1-500 orang")]
    public int Capacity { get; set; }
    
    [Required(ErrorMessage = "Lokasi wajib diisi")]
    [StringLength(200, ErrorMessage = "Lokasi maksimal 200 karakter")]
    public string Location { get; set; } = string.Empty;
    
    public string? Description { get; set; }
}

public class UpdateRoomDto : CreateRoomDto
{
    public int Id { get; set; }
    public RoomStatus Status { get; set; }
}

public class RoomResponseDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Location { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}