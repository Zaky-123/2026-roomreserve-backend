ASP.NET Core Web API untuk Sistem Peminjaman Ruangan Kampus.  
Dibangun untuk mengatasi permasalahan pencatatan peminjaman ruangan yang masih manual dan tidak terpusat.

## 📋 Fitur

### ✅ Sudah Tersedia
- **Manajemen Ruangan** (CRUD)
  - Tambah ruangan baru
  - Lihat daftar ruangan (dengan pagination & search)
  - Detail ruangan
  - Ubah data ruangan
  - Hapus ruangan (soft delete)
- **Database SQLite** - Ringan dan tanpa instalasi
- **Swagger Documentation** - Dokumentasi API interaktif
- **CORS Configuration** - Siap diintegrasi dengan React frontend
- **Validasi Input** - Dengan pesan error dalam Bahasa Indonesia
- **Soft Delete** - Data tidak benar-benar hapus

### 🚧 Dalam Pengembangan
- **Manajemen Peminjaman** (Booking CRUD)
- **Status Peminjaman** (Pending, Approved, Rejected, Completed)
- **Riwayat dan Filter Peminjaman**
- **Autentikasi Pengguna**

## 🛠 Tech Stack

- **ASP.NET Core 10.0.0** - Web API framework
- **Entity Framework Core 10.0.0** - ORM untuk database
- **SQLite** - Database ringan (development)
- **Swashbuckle (Swagger)** - Dokumentasi API
- **CORS** - Untuk integrasi frontend

## 📁 Struktur Project
backend/
├── Controllers/ # API Controllers
│ └── RoomsController.cs
├── Data/ # Database Context
│ └── AppDbContext.cs
├── DTOs/ # Data Transfer Objects
│ └── RoomDto.cs
├── Migrations/ # EF Core Migrations
├── Models/ # Entity Models
│ ├── Room.cs
│ └── Booking.cs
├── Properties/ # Project properties
├── appsettings.json # Konfigurasi aplikasi
├── Program.cs # Entry point
├── RoomReserve.Api.csproj
└── README.md

## ⚙️ Instalasi

### Prasyarat
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)

### Langkah-langkah

1. **Clone repository**
   ```bash
   git clone https://github.com/Zaky-123/2026-roomreserve-backend.git
   cd 2026-roomreserve-backend/backend

2. **Restro Packages**
    dotnet restore

3. **Setup database (SQLite)**
    #Buat migrasi (jika belum ada)
    dotnet ef migrations add InitialCreate

    #Update database (akan buat file RoomReserve.db)
    dotnet ef database update

4. **Jalankan Aplikasi**
    dotnet run

5. **Akses API**
    http://localhost:5243/swagger
    http://localhost:5243/api/rooms

##  Konfigurasi Environment
appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=RoomReserve.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}

.env.example
# Database Configuration
DB_CONNECTION=Data Source=RoomReserve.db

## API Settings
API_URL=http://localhost:5243
FRONTEND_URL=http://localhost:3000

## Endpoint Rooms
Method	    Endpoint	        Deskripsi
GET	        /api/rooms          List semua ruangan (dengan  pagination & search)
GET	        /api/rooms/{id}	    Detail ruangan by ID
POST	    /api/rooms	        Tambah ruangan baru
PUT	        /api/rooms/{id}	    Update data ruangan
DELETE	    /api/rooms/{id}	    Hapus ruangan (soft delete)

🤝 Contributing
Fork repository
Buat branch feature: git checkout -b feature/nama-fitur
Commit perubahan: git commit -m "feat: menambahkan fitur baru"
Push ke branch: git push origin feature/nama-fitur
Buat Pull Request ke branch develop


📝 Lisensi
MIT License - silakan digunakan untuk keperluan pembelajaran.


👤 Author
Zaky - @Zaky-123