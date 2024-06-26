using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace server_elearning.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string TenDangNhap { get; set; }

        [Required]
        public string MatKhau { get; set; }
    }
}
