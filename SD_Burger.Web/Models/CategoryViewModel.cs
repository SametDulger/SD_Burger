using System;
using System.ComponentModel.DataAnnotations;

namespace SD_Burger.Web.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Resim URL'si en fazla 500 karakter olabilir.")]
        [Display(Name = "Resim URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Display(Name = "Görüntüleme Sırası")]
        public int DisplayOrder { get; set; }

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
    }

    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Resim URL'si en fazla 500 karakter olabilir.")]
        [Display(Name = "Resim URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Display(Name = "Görüntüleme Sırası")]
        public int DisplayOrder { get; set; }
    }

    public class UpdateCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Resim URL'si en fazla 500 karakter olabilir.")]
        [Display(Name = "Resim URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Display(Name = "Görüntüleme Sırası")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
    }
} 