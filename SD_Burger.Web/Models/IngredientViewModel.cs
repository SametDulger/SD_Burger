using System;
using System.ComponentModel.DataAnnotations;

namespace SD_Burger.Web.Models
{
    public class IngredientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Malzeme adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Malzeme adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Malzeme Adı")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birim zorunludur.")]
        [StringLength(20, ErrorMessage = "Birim en fazla 20 karakter olabilir.")]
        [Display(Name = "Birim")]
        public string Unit { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birim fiyatı zorunludur.")]
        [Range(0, double.MaxValue, ErrorMessage = "Birim fiyatı 0'dan büyük olmalıdır.")]
        [Display(Name = "Birim Fiyatı")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Minimum stok zorunludur.")]
        [Range(0, int.MaxValue, ErrorMessage = "Minimum stok 0'dan büyük olmalıdır.")]
        [Display(Name = "Minimum Stok")]
        public int MinimumStock { get; set; }

        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? CreatedAt { get; set; } // Alias for CreatedDate

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? UpdatedAt { get; set; } // Alias for UpdatedDate

        [Display(Name = "Mevcut Stok")]
        public decimal CurrentStock { get; set; }
    }

    public class CreateIngredientViewModel
    {
        [Required(ErrorMessage = "Malzeme adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Malzeme adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Malzeme Adı")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birim zorunludur.")]
        [StringLength(20, ErrorMessage = "Birim en fazla 20 karakter olabilir.")]
        [Display(Name = "Birim")]
        public string Unit { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birim fiyatı zorunludur.")]
        [Range(0, double.MaxValue, ErrorMessage = "Birim fiyatı 0'dan büyük olmalıdır.")]
        [Display(Name = "Birim Fiyatı")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Minimum stok zorunludur.")]
        [Range(0, int.MaxValue, ErrorMessage = "Minimum stok 0'dan büyük olmalıdır.")]
        [Display(Name = "Minimum Stok")]
        public int MinimumStock { get; set; }
    }

    public class UpdateIngredientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Malzeme adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Malzeme adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Malzeme Adı")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birim zorunludur.")]
        [StringLength(20, ErrorMessage = "Birim en fazla 20 karakter olabilir.")]
        [Display(Name = "Birim")]
        public string Unit { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birim fiyatı zorunludur.")]
        [Range(0, double.MaxValue, ErrorMessage = "Birim fiyatı 0'dan büyük olmalıdır.")]
        [Display(Name = "Birim Fiyatı")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Minimum stok zorunludur.")]
        [Range(0, int.MaxValue, ErrorMessage = "Minimum stok 0'dan büyük olmalıdır.")]
        [Display(Name = "Minimum Stok")]
        public int MinimumStock { get; set; }

        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
    }
} 