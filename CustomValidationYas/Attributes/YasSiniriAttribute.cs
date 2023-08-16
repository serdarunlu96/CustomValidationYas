using System.ComponentModel.DataAnnotations;

namespace CustomValidationYas.Attributes
{
    public class YasSiniriAttribute : ValidationAttribute
    {
		public YasSiniriAttribute(int yas)
		{
			Yas = yas;
		}
        public int Yas { get; set; }

        public string HataMesajı => $"Yas en az {Yas} olmalıdır.";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime? tarih = value as DateTime?;

            if(tarih != null && YasHesapla(tarih.Value) < Yas)
                return new ValidationResult(HataMesajı);

            return ValidationResult.Success;
        }

        private int YasHesapla(DateTime deger)
        {
            DateTime bugun = DateTime.Today;

            int yas = bugun.Year - deger.Year;

            if(deger.Month > bugun.Month || deger.Month == bugun.Month && deger.Day > bugun.Day)
                return yas-1;

            return yas;
        }
    }
}
