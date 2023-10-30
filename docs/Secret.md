# Secrets trong PetStore project
.NET 6.0 tiếp tục hỗ trợ User Secrets cho việc quản lý cấu hình trong ứng dụng. User Secrets là một cơ chế cho phép bạn lưu trữ các thông tin nhạy cảm như chuỗi kết nối cơ sở dữ liệu, khóa API, hoặc bất kỳ thông tin bí mật nào ngoài mã nguồn của bạn, mà không cần lưu trữ chúng trực tiếp trong mã nguồn hoặc tệp cấu hình chung.

Để sử dụng User Secrets trong một dự án .NET 6.0, bạn cần thực hiện các bước sau:

* Thêm gói User Secrets:
👉 Đảm bảo rằng dự án của bạn đã thêm gói Microsoft Extensions.Configuration.UserSecrets thông qua NuGet Package Manager hoặc file .csproj.

* Enable User Secrets:
📂 Mở dự án của bạn trong môi trường phát triển (Visual Studio hoặc Visual Studio Code) và chạy lệnh sau trong cửa sổ dòng lệnh:
``` dotnet user-secrets init ```

* Thêm các thông tin bí mật:
🔐 Sử dụng lệnh dotnet user-secrets set để thêm thông tin bí mật vào dự án của bạn. Ví dụ:
```dotnet user-secrets set "ConnectionStrings:MyDatabase" "Server=myserver;Database=mydb;User=myuser;Password=mypassword" ``` 
Thay thế "ConnectionStrings:MyDatabase" và chuỗi kết nối thực tế bằng thông tin của bạn.

⚠ Lưu ý rằng User Secrets được sử dụng trong quá trình phát triển và làm việc tốt cho môi trường phát triển. Trong môi trường sản phẩm hoặc triển khai, bạn nên sử dụng cơ chế quản lý cấu hình an toàn hơn như Azure Key Vault hoặc biến môi trường để bảo vệ thông tin bí mật.

* Sử dụng lệnh sau để liệt kê tất cả thông tin User Secrets:
```dotnet user-secrets list```
Lệnh này sẽ hiển thị danh sách các thông tin User Secrets và giá trị của chúng.

* Nếu bạn muốn hiển thị một thông tin cụ thể, bạn có thể sử dụng lệnh sau và thay thế <Key> bằng tên khóa bạn muốn xem:
```dotnet user-secrets get <Key>```
Ví dụ:
```dotnet user-secrets get "ConnectionStrings:MyDatabase"``` 
Điều này sẽ hiển thị giá trị của khóa "ConnectionStrings:MyDatabase" trong User Secrets.

Vui lòng khi chạy project vào appsettings.Development.json để setting những trường còn trống trong user secret của mình.
```sh
dotnet user-secrets set "JwtSettings:MyDatabase" "your-jwt-secret-key"

dotnet user-secrets set "EmailSettings:Username" "your-email-username"

dotnet user-secrets set "EmailSettings:Password" "your-email-password"
```