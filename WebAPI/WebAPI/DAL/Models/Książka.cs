using System.ComponentModel.DataAnnotations;

namespace WebAPI.DAL.Models
{
    public class Książka
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [MaxLength(100, ErrorMessage = "Tytuł może mieć maksymalnie 100 znaków.")]
        public string Tytul { get; set; }
        public string Autor { get; set; }
        [Required(ErrorMessage = "Gatunek jest wymagany.")]
        public string Gatunek { get; set;}
        [Range(0, 2025, ErrorMessage = "Rok nie może być większy niż 2025.")]
        public int Rok { get; set;}    
    }
}
