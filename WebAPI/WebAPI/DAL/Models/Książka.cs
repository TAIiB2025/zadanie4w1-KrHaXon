namespace WebAPI.DAL.Models
{
    public class Książka
    {
        public int Id { get; set; }
        public string Tytul { get; set; }
        public string Autor { get; set; }
        public string Gatunek { get; set;}
        public int Rok { get; set;}    
    }
}
