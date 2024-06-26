using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace server_elearning.Models
{
    public class FResult
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? IDLS { get; set; }
        public int? NVID { get; set; }
        public int? VTID  { get; set; }
        public DateTime ThangDG { get; set; }
        public DateTime NgayDGGN { get; set; }
        public int? DAT { get; set; } = 0;
        public int? KDAT { get; set; } = 0;
        public int? VUOT { get; set; } = 0;
        public int? KDGia { get; set; } = 0;
        public int? CHUADG { get; set; } = 0;
        public int? TONGNL { get; set; } = 0;
        public string FilePath { get; set; }
        public string TenVTKNL { get; set; } = "";
        public string TenViTri { get; set; } = "";
        [NotMapped]
        public string ThangDGStr { get; set; }
    }
    public class returnFResult
    {
        public List<FResult> fResults { get; set; }
        public int total { get; set; }

    }
}
