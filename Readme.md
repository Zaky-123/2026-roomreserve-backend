ASP.NET Core Web API untuk Sistem Peminjaman Ruangan Kampus.  
Dibangun untuk mengatasi permasalahan pencatatan peminjaman ruangan yang masih manual dan tidak terpusat.

## 📋 Fitur Lengkap (v1.1.0)

### ✅ Manajemen Ruangan (Room CRUD)
- Tambah ruangan baru
- Lihat daftar ruangan (dengan pagination & search)
- Detail ruangan
- Ubah data ruangan
- Hapus ruangan (soft delete)

### ✅ Manajemen Peminjaman (Booking CRUD)
- Tambah peminjaman baru dengan validasi
- Lihat daftar peminjaman dengan filter lengkap
- Detail peminjaman
- Ubah data peminjaman (hanya jika status Pending)
- Hapus peminjaman (soft delete, hanya jika Pending)
- **Conflict Detection**: Cegah double booking otomatis

### ✅ Status Management
- **Pending** (kuning) - Menunggu persetujuan
- **Approved** (hijau) - Disetujui
- **Rejected** (merah) - Ditolak
- **Cancelled** (abu-abu) - Dibatalkan
- **Completed** (biru) - Selesai

**Flow Status:**
Pending → Approved → Completed
Pending → Rejected
Pending → Cancelled
Approved → Cancelled


### ✅ Filter, Search, Sorting (GET /api/bookings)
| Parameter | Tipe | Deskripsi | Contoh |
|-----------|------|-----------|--------|
| `roomId` | integer | Filter by ID ruangan | `roomId=1` |
| `status` | string | Pending/Approved/Rejected/Cancelled/Completed | `status=Pending` |
| `startDate` | datetime | Filter tanggal mulai (>=) | `startDate=2026-02-01` |
| `endDate` | datetime | Filter tanggal selesai (<=) | `endDate=2026-02-28` |
| `search` | string | Cari di nama/email/tujuan | `search=john` |
| `sortBy` | string | startTime/endTime/createdAt/borrowerName/status | `sortBy=startTime` |
| `sortOrder` | string | asc / desc | `sortOrder=desc` |
| `page` | integer | Halaman | `page=1` |
| `pageSize` | integer | Jumlah per halaman | `pageSize=10` |

### ✅ Riwayat Status (Booking History)
- Setiap perubahan status tercatat otomatis
- Lihat riwayat lengkap: siapa, kapan, dari status apa, ke status apa
- Endpoint: `GET /api/bookings/{id}/history`

## 🛠 Tech Stack

- **ASP.NET Core 8.0** - Web API framework
- **Entity Framework Core 8.0** - ORM untuk database
- **SQLite** - Database ringan (development)
- **Swashbuckle (Swagger)** - Dokumentasi API
- **CORS** - Untuk integrasi frontend

## 📁 Struktur Project
backend/
├── Controllers/ # API Controllers
│ ├── RoomsController.cs
│ └── BookingsController.cs
├── Data/ # Database Context
│ └── AppDbContext.cs
├── DTOs/ # Data Transfer Objects
│ ├── RoomDto.cs
│ ├── BookingDto.cs
│ └── BookingStatusDto.cs
├── Migrations/ # EF Core Migrations
├── Models/ # Entity Models
│ ├── Room.cs
│ ├── Booking.cs
│ └── BookingHistory.cs
├── Properties/ # Project properties
├── appsettings.json # Konfigurasi aplikasi
├── Program.cs # Entry point
├── README.md
└── CHANGELOG.md


## ⚙️ Instalasi

### Prasyarat
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)

### Langkah-langkah

1. **Clone repository**
   ```bash
   git clone https://github.com/Zaky-123/2026-roomreserve-backend.git
   cd 2026-roomreserve-backend/backend

2. **Restore packages**
  dotnet restore

3. **Setup database (SQLite)**
  # Update database (akan buat file RoomReserve.db)
  dotnet ef database update

4. **Jalankan Aplikasi**
  dotnet run

5. **Akses Api**
  http://localhost:5243/swagger
  http://localhost:5243/api/rooms
  http://localhost:5243/api/bookings

🤝 Contributing
1.Fork repository
2.Buat branch feature: git checkout -b feature/nama-fitur
3.Commit perubahan: git commit -m "feat: menambahkan fitur baru"
5.Push ke branch: git push origin feature/nama-fitur
6.Buat Pull Request ke branch develop


📝 Lisensi
MIT License - silakan digunakan untuk keperluan pembelajaran.


👤 Author
Zaky - @Zaky-123