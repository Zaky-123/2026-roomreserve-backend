using System.ComponentModel.DataAnnotations;
using RoomReserve.Api.Models;

namespace RoomReserve.Api.DTOs;

public class CreateBookingDto
{
    [Required(ErrorMessage = "Ruangan wajib dipilih")]
    public int RoomId { get; set; }
    
    [Required(ErrorMessage = "Nama peminjam wajib diisi")]
    [StringLength(100, ErrorMessage = "Nama peminjam maksimal 100 karakter")]
    public string BorrowerName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email wajib diisi")]
    [EmailAddress(ErrorMessage = "Format email tidak valid")]
    public string BorrowerEmail { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Nomor telepon wajib diisi")]
    [StringLength(20, ErrorMessage = "Nomor telepon maksimal 20 karakter")]
    [Phone(ErrorMessage = "Format nomor telepon tidak valid")]
    public string BorrowerPhone { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Waktu mulai wajib diisi")]
    public DateTime StartTime { get; set; }
    
    [Required(ErrorMessage = "Waktu selesai wajib diisi")]
    public DateTime EndTime { get; set; }
    
    [StringLength(500, ErrorMessage = "Tujuan maksimal 500 karakter")]
    public string? Purpose { get; set; }
}

public class UpdateBookingDto : CreateBookingDto
{
    public int Id { get; set; }
}

public class BookingResponseDto
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string RoomName { get; set; } = string.Empty;
    public string RoomCode { get; set; } = string.Empty;
    public string BorrowerName { get; set; } = string.Empty;
    public string BorrowerEmail { get; set; } = string.Empty;
    public string BorrowerPhone { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Purpose { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}