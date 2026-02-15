**Tanggal:** 15 Februari 2026
**Penguji:** Zaky
**Aplikasi:** RoomReserve.Api
**Base URL:** http://localhost:5243

## Ringkasan Pengujian

| No | Skenario | Endpoint | Status | Keterangan |
|----|----------|----------|--------|------------|
| 1 | GET all rooms (tanpa filter) | GET /api/rooms | ✅ OK | Menampilkan 3 data seed |
| 2 | GET with search "seminar" | GET /api/rooms?search=seminar | ✅ OK | Menampilkan 1 data |
| 3 | POST create room (valid) | POST /api/rooms | ✅ 201 Created | Data tersimpan |
| 4 | POST create room (duplicate code) | POST /api/rooms | ✅ 400 Bad Request | Validasi berfungsi |
| 5 | POST create room (required fields) | POST /api/rooms | ✅ 400 Bad Request | Validasi berfungsi |
| 6 | GET by ID (existing) | GET /api/rooms/4 | ✅ OK | Data ditemukan |
| 7 | GET by ID (not found) | GET /api/rooms/999 | ✅ 404 Not Found | Pesan error sesuai |
| 8 | PUT update room (valid) | PUT /api/rooms/4 | ✅ 204 No Content | Data terupdate |
| 9 | PUT update (ID mismatch) | PUT /api/rooms/4 | ✅ 400 Bad Request | Validasi berfungsi |
| 10 | DELETE room | DELETE /api/rooms/4 | ✅ 204 No Content | Soft delete |
| 11 | Verifikasi soft delete | GET /api/rooms/4 | ✅ 404 Not Found | Data tidak muncul |

## Kesimpulan

✅ **Semua skenario pengujian berhasil**  
✅ **Validasi input berfungsi dengan baik**  
✅ **Soft delete diimplementasikan dengan benar**  
✅ **Error handling memberikan pesan yang informatif**  
✅ **Pagination dan search berfungsi sesuai spesifikasi**

## Catatan

- Aplikasi berjalan di port 5243 (HTTP)
- Database menggunakan SQLite (RoomReserve.db)
- Seed data: 3 ruangan (R101, R102, LAB01)
- Semua endpoint dapat diakses via Swagger