using CustomValidationYas.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomValidationYas.Controllers
{
    public class MusterilerController : Controller
    {
        public MusterilerController(IWebHostEnvironment env) 
        {
            _env = env;
        }

        static readonly List<Musteri> musteriler = new()
        {
            new Musteri() { Ad = "Ali" , DogumTarihi = new DateTime(1990, 7 , 22) },
            new Musteri() { Ad = "Can" , DogumTarihi = new DateTime(1986, 4 , 3) },
            new Musteri() { Ad = "Cem" , DogumTarihi = new DateTime(2005, 2, 10) },
            new Musteri() { Ad = "Ece" , DogumTarihi = new DateTime(1997, 12, 10) },
        };

        private readonly IWebHostEnvironment _env;

        public IActionResult Index(string? sonuc)
        {
            ViewBag.Mesaj = MesajaKararVer(sonuc);
            return View(musteriler);
        }

        private string MesajaKararVer(string? sonuc)
        {
            switch (sonuc)
            {
                case "ekledi":
                    return "Yeni musteri basarı ile olusturuldu";
                case "silindi":
                    return "Musteri basarı ile silindi";
                case "duzenlendi":
                    return "Musteri basarı ile guncellendi";
                default:
                    return "";
            }
        }

        public IActionResult Yeni()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Yeni(Musteri musteri)
        {
            if (musteri.ProfilFoto != null)
            {
                if (!musteri.ProfilFoto.ContentType.StartsWith("image/"))
                    ModelState.AddModelError("ProfilFoto", "Gecersiz bir resim dosyası.");
                else if (musteri.ProfilFoto.Length > (1024 * 1024))
                    ModelState.AddModelError("ProfilFoto", "Resim boyutu ici maximum boyut 1MB'dır.");
            }

            if (ModelState.IsValid)
            {
                if (musteri.ProfilFoto != null)
                    musteri.FotoAd = ResmiKaydet(musteri.ProfilFoto);
                musteriler.Add(musteri);
                return RedirectToAction(nameof(Index), new { sonuc = "eklendi" });
            }
            return View();
        }

        private string ResmiKaydet(IFormFile profilFoto)
        {
            string ext = Path.GetExtension(profilFoto.FileName);

            string fname = Path.Combine(Guid.NewGuid().ToString() + ext);

            string yol = Path.Combine(_env.WebRootPath, "img" , fname);

            using (var fileStream = System.IO.File.Create(yol))
            {
                profilFoto.CopyTo(fileStream);
            }

            return fname;

        }
    }
}
