using CustomValidationYas.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomValidationYas.Models
{
    public class Musteri
    {
        [Display(Name = "Musteri No")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [MaxLength(50,ErrorMessage ="{0} alanı en fazla {1} karakter içerebilir.")]
        [Display(Name = "İsim")]
        public string Ad { get; set; } = null!;

        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        [YasSiniri(18)]
        public DateTime? DogumTarihi { get; set; }

        [Display(Name = "Fotograf")]
        public string FotoAd { get; set; } = "user.jpg";

        [Display(Name = "Profil Resmi")]
        public IFormFile? ProfilFoto { get; set; }
    }
}
