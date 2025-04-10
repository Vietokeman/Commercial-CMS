# CMS Project - ASP.NET Core & Angular

## Giới thiệu
Dự án này là một hệ thống quản lý nội dung (CMS) được xây dựng bằng ASP.NET Core cho backend và Angular cho frontend. Hệ thống hỗ trợ quản lý bài viết, danh mục, người dùng và quyền truy cập, cung cấp một nền tảng linh hoạt để tạo và quản lý nội dung một cách hiệu quả.

## Công nghệ sử dụng
- **Backend**: ASP.NET Core (.NET 8.0), Entity Framework Core, ASP.NET Identity, JWT Authentication
- **Frontend**: Angular, CoreUI, TypeScript
- **Database**: SQL Server
- **API**: RESTful API với NSwag để generate API Client cho Angular
- **Quản lý mã nguồn**: GitHub

## Tính năng chính
### **1. Backend (ASP.NET Core)**
- Xác thực và phân quyền với JWT Token
- Quản lý người dùng và quyền truy cập (Role-based Access Control)
- CRUD bài viết, danh mục và loạt bài
- Tích hợp Entity Framework Core và cấu hình Repository + UnitOfWork
- Hỗ trợ phân trang và AutoMapper

### **2. Frontend (Angular)**
- Giao diện Admin với CoreUI
- Xác thực và phân quyền trên giao diện người dùng
- Hiển thị danh sách bài viết, chi tiết bài viết, bài viết theo danh mục và tag
- Quản lý tài khoản: đăng ký, đăng nhập, đổi mật khẩu, cập nhật hồ sơ
- Quản lý nội dung: thêm/sửa/xóa bài viết, duyệt bài
- Hệ thống quản lý nhuận bút cho tác giả

### **3. DevOps & Triển khai**
- Cấu hình môi trường phát triển
- Deploy ứng dụng lên server
- Quản lý source code trên GitHub

## Cài đặt & Chạy dự án
### **Yêu cầu hệ thống**
- .NET 8.0 SDK
- Node.js & Angular CLI
- SQL Server
- Visual Studio 2022 / VS Code

### **Cách chạy dự án**
#### **1. Backend**
```bash
cd backend
# Cài đặt dependencies
dotnet restore
# Chạy migration
dotnet ef database update
# Chạy ứng dụng
dotnet run
```

#### **2. Frontend**
```bash
cd frontend
# Cài đặt dependencies
npm install
# Chạy ứng dụng
ng serve --open
```

## Hướng dẫn sử dụng
1. Đăng ký tài khoản và đăng nhập
2. Quản lý danh mục, bài viết từ trang Admin
3. Phân quyền người dùng
4. Tạo bài viết mới, duyệt bài
5. Hiển thị bài viết theo danh mục, tag

## Liên hệ & Đóng góp
Nếu bạn có bất kỳ đề xuất hoặc muốn đóng góp, vui lòng tạo Pull Request hoặc liên hệ qua GitHub.

---
**Tác giả**: Vietokeman
**GitHub Repository**: https://github.com/Vietokeman/Commercial-CMS
**Facebook**: https://www.facebook.com/vietphomaique123/
