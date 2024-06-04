using B09.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace B09.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly AlbumService _service;

        public List<Album> Pretraga { get; set; }
        public List<SelectListItem> Zanri { get; set; }
        public List<SelectListItem> Godine { get; set; }
        public IndexModel(IWebHostEnvironment env, AlbumService service)
        {
            _env = env;
            _service = service;
            Zanri = new List<SelectListItem>();
            Godine = new List<SelectListItem>();
        }

        [BindProperty]
        public string izvodjac { get; set; }

        [BindProperty]
        public string nazivAlbuma { get; set; }

        [BindProperty]
        public string zanr { get; set; }

        [BindProperty]
        public string godina { get; set; }

        [BindProperty]
        public string kuca { get; set; }

        public void OnGet()
        {
            if(Pretraga == null)
            {
                string[] putanja = Directory.GetFiles(Path.Combine(this._env.WebRootPath, "file"));
                var sr = new StreamReader(putanja[0]);
                var lajna = sr.ReadLine();
                while (lajna != null)
                {
                    var podaci = lajna.Split('|');
                    var album = new Album()
                    {
                        Izvodjac = podaci[0],
                        NazivAlbuma = podaci[1],
                        Zanr = podaci[2],
                        Godina = podaci[3],
                        Kuca = podaci[4],
                        Slika = podaci[5],
                    };
                    _service.Albums.Add(album);
                    lajna = sr.ReadLine();
                }
                Pretraga = _service.Albums;
            }
            foreach (var item in _service.Albums)
            {
                var zanr = Zanri.FirstOrDefault(x => x.Value == item.Zanr);
                if (zanr == null)
                    Zanri.Add(new SelectListItem() { Value = item.Zanr, Text = item.Zanr });
            }

            foreach (var item in _service.Albums)
            {
                var godina = Godine.FirstOrDefault(x => x.Value == item.Godina);
                if (godina == null)
                    Godine.Add(new SelectListItem() { Value = item.Godina, Text = item.Godina });
            }
        }

        public void OnPost()
        {
            if (izvodjac == null) izvodjac = "";
            if (nazivAlbuma == null) nazivAlbuma = "";
            if (zanr == null) zanr = "";
            if (godina == null) godina = "";
            if (kuca == null) kuca = "";

            string[] pretraga = new string[]
            {
                izvodjac,
                nazivAlbuma,
                zanr,
                godina,
                kuca,
            };

            Pretraga = _service.Pretraga(pretraga);
            OnGet();
        }
    }
}
