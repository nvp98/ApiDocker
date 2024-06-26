using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace server_elearning.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse(TaiKhoan user, string jwtToken, string refreshToken)
        {
            Id = user.ID;
            Username = user.TenDangNhap;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
