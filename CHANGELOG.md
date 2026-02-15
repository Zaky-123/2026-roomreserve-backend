Semua perubahan penting pada proyek ini akan dicatat di file ini.

Format berdasarkan [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
dan proyek ini mengikuti [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2026-02-15

### Added
- **Fitur Manajemen Ruangan (CRUD)**
  - Model Room dengan field: Code, Name, Capacity, Location, Status, Description
  - Enum RoomStatus: Available, UnderMaintenance, Occupied
  - Endpoint API:
    - `GET /api/rooms` - List ruangan dengan pagination dan search
    - `GET /api/rooms/{id}` - Detail ruangan
    - `POST /api/rooms` - Tambah ruangan baru
    - `PUT /api/rooms/{id}` - Update ruangan
    - `DELETE /api/rooms/{id}` - Hapus ruangan (soft delete)
  - Validasi input menggunakan DataAnnotations
  - DTOs untuk request dan response
  - Soft delete dengan field DeletedAt

- **Database**
  - SQLite sebagai database provider
  - Migration untuk tabel Rooms
  - Seed data dengan 3 ruangan contoh:
    - R101: Ruang Seminar A (kapasitas 50)
    - R102: Ruang Kelas 201 (kapasitas 40)
    - LAB01: Lab Komputer (kapasitas 30)

- **Dokumentasi API**
  - Swagger UI terintegrasi
  - Dokumentasi endpoint lengkap

- **Konfigurasi**
  - CORS untuk frontend React (http://localhost:3000)
  - Environment variables template (.env.example)
  - .gitignore untuk .NET dan file sensitif

- **Dokumentasi Proyek**
  - README.md dengan panduan instalasi dan penggunaan
  - CHANGELOG.md untuk tracking perubahan

### Changed
- Migrasi dari WeatherForecast template ke project struktur yang sesuai
- Update Program.cs dengan konfigurasi SQLite dan Swagger
- Rename dan restrukturisasi folder proyek

### Removed
- WeatherForecastController.cs dan WeatherForecast.cs (template default)
- HTTPS redirection sementara (untuk development)

### Fixed
- Error build terkait missing package Swashbuckle.AspNetCore
- Warning tentang dynamic values di seed data (diganti dengan nilai statis)
- CS0246 error dengan menambahkan using yang tepat

### Security
- Implementasi soft delete untuk keamanan data
- Validasi input mencegah SQL injection

## [Unreleased]

### Rencana Fitur
- [ ] Manajemen Peminjaman (Booking CRUD)
- [ ] Status peminjaman (Pending, Approved, Rejected, Completed)
- [ ] Riwayat dan filter peminjaman
- [ ] Autentikasi dan otorisasi pengguna
- [ ] Unit testing

---

### Catatan Rilis
**v1.0.0** adalah rilis pertama dari Room Reservation API. 
Fitur utama yang tersedia adalah manajemen ruangan dengan 
operasi CRUD lengkap, validasi, soft delete, dan dokumentasi API via Swagger.