namespace QLSinhVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SinhVien")]
    public partial class SinhVien
    {
        public long id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Họ Và Tên")]
        public string HoTen { get; set; }
        
        [Required]
        [Display(Name = "Ngày Sinh")]
        [Column(TypeName = "date")]
        public DateTime NamSinh { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Khóa")]
        public string KhoaHoc { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Tên Lớp")]
        public long IdLopHoc { get; set; }

        public long IdPermission { get; set; } = 1;

        public long? IdThongBao { get; set; }

        public int IsDelete { get; set; }

        public virtual LopHoc LopHoc { get; set; }

        public virtual Permission Permission { get; set; }

        public virtual ThongBao ThongBao { get; set; }
    }
}
