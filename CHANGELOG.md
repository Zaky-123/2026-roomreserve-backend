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

## 2. CHANGELOG.md (Update untuk v1.1.0)

## [1.1.0] - 2026-02-16

### ✨ Fitur Baru

#### Booking CRUD
- **Create Booking** - Tambah peminjaman dengan validasi:
  - Cek ketersediaan ruangan (conflict detection)
  - Validasi waktu (start < end, tidak boleh di masa lalu)
  - Status awal: Pending
- **Read Bookings** - List peminjaman dengan filter lengkap
- **Update Booking** - Ubah data (hanya jika status Pending)
- **Delete Booking** - Hapus (soft delete, hanya jika Pending)

#### Status Management
- **Endpoint PATCH /api/bookings/{id}/status** - Update status
- **Status Flow** yang valid:
  - Pending → Approved/Rejected/Cancelled
  - Approved → Completed/Cancelled
  - Rejected/Cancelled/Completed (final, tidak bisa berubah)
- **Validasi transisi** - Error jika tidak sesuai aturan

#### Booking History
- **Model BookingHistory** untuk tracking perubahan status
- **Auto-record** setiap kali status berubah
- **Endpoint GET /api/bookings/{id}/history** - Lihat riwayat
- Riwayat diurutkan descending by ChangedAt

#### Filter, Search, Sorting (GET /api/bookings)
- **Filter by room** - `roomId`
- **Filter by status** - `status` (Pending/Approved/Rejected/Cancelled/Completed)
- **Filter by date range** - `startDate`, `endDate`
- **Search** - `search` di borrowerName, borrowerEmail, purpose
- **Sorting** - `sortBy` (startTime/endTime/createdAt/borrowerName/status) dan `sortOrder` (asc/desc)
- **Pagination** - `page` dan `pageSize`

### 📚 Dokumentasi
- **README.md** diperbarui dengan:
  - Semua fitur v1.1.0
  - Dokumentasi lengkap parameter filter
  - Contoh request untuk semua endpoint
  - Tabel status dengan warna
- **Swagger** updated untuk semua endpoint baru

### 🔧 Improvements
- Conflict detection lebih akurat
- Validasi transisi status menggunakan pattern matching
- Error messages lebih informatif (dalam Bahasa Indonesia)
- Performance query dengan indexing

### 🐛 Fixed
- Bug duplicate booking detection
- Bug status transition validation
- Null reference di BookingHistory

## [1.0.0] - 2026-02-15

### ✨ Fitur Awal

#### Room CRUD
- **Create Room** - Tambah ruangan dengan validasi
- **Read Rooms** - List ruangan dengan pagination & search
- **Update Room** - Ubah data ruangan
- **Delete Room** - Soft delete ruangan
- **Fields**: Code, Name, Capacity, Location, Status, Description

#### Database
- SQLite database provider
- Migration untuk tabel Rooms
- Seed data: 3 ruangan contoh (R101, R102, LAB01)
- Soft delete dengan DeletedAt field

#### Dokumentasi API
- Swagger UI terintegrasi
- Dokumentasi endpoint Rooms

#### Konfigurasi
- CORS untuk frontend React (http://localhost:3000)
- Environment variables template (.env.example)
- .gitignore untuk .NET dan file sensitif

### 🔧 Improvements
- Validasi input menggunakan DataAnnotations
- Error handling dengan logging
- Response menggunakan DTO

### 🐛 Fixed
- Error build terkait missing package Swashbuckle.AspNetCore
- Warning tentang dynamic values di seed data
- CS0246 error dengan menambahkan using yang tepat


## 📊 Ringkasan Versi

| Versi | Tanggal | Fitur Utama |
|-------|---------|-------------|
| v1.1.0 | 2026-02-16 | Booking CRUD + Status Management + Filter |
| v1.0.0 | 2026-02-15 | Room CRUD + Database + Swagger |

## 👤 Kontributor

- **Zaky** - Pengembang utama