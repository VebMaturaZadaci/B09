using B09.Data;

namespace B09
{
    public class AlbumService
    {
        public List<Album> Albums { get; set; }

        public AlbumService()
        {
            Albums = new List<Album>();
        }

        public List<Album> Pretraga(string[] pretraga)
        {
            var lista = new List<Album>();

            foreach (var item in Albums)
            {
                if (item.Izvodjac.ToLower().Contains(pretraga[0].ToLower()) 
                    && item.NazivAlbuma.ToLower().Contains(pretraga[1].ToLower())
                    && item.Zanr.ToLower().Contains(pretraga[2].ToLower()) 
                    && item.Godina.ToLower().Contains(pretraga[3].ToLower())
                    && item.Kuca.ToLower().Contains(pretraga[4].ToLower()))
                {
                    lista.Add(item);
                }
            }

            return lista;
        }
    }
}
