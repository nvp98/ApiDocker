using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server_elearning.Models
{
    public class DanhSachDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDDSĐA { get; set; }
        public string TenĐA { get; set; }
    }
}
