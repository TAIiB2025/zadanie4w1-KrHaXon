using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BLL;
using WebAPI.BLL.ModelsDTO;
using WebAPI.BLL_Memory.Extensions;
using WebAPI.DAL.Models;

namespace WebAPI.BLL_Memory
{
    public class KsiążkaService : IKsiązkaService
    {
        private static readonly string[] DozwoloneGatunki = { "fantasy", "romans", "dystopia" };
        private static int IdGen = 1;
        private static readonly List<Książka> ksiazkiDbSet =
            [
                new Książka()
                {
                    Id = IdGen++,
                    Tytul = "Zbrodnia i kara",
                    Autor = "Fiodor Dostojewski",
                    Gatunek = "Powieść psychologiczna",
                    Rok = 1866
                },
                new Książka()
                {
                    Id = IdGen++,
                    Tytul = "Pan Tadeusz",
                    Autor = "Adam Mickiewicz",
                    Gatunek = "Epopeja narodowa",
                    Rok = 1834
                },
                new Książka()
                {
                    Id= IdGen++,
                    Tytul = "Rok 1984",
                    Autor = "George Orwell",
                    Gatunek = "Dystopia",
                    Rok = 1949
                },
                new Książka()
                {
                    Id=IdGen++,
                    Tytul = "Wiedźmin: Ostatnie życzenie",
                    Autor = "Andrzej Sapkowski",
                    Gatunek = "Fantasy",
                    Rok = 1993
                },
                new Książka()
                {
                    Id=IdGen++,
                    Tytul = "Duma i uprzedzenie", 
                    Autor = "Jane Austen", 
                    Gatunek = "Romans", 
                    Rok = 1813
                }
            ];
        public IEnumerable<KsiążkaDTO> Get()
        {
            return ksiazkiDbSet.Select(ks => ks.ToKsiążkaDTO());
        }
        public KsiążkaDTO GetById(int id)
        {
            Książka? ksiazka = ksiazkiDbSet.Find(ks => ks.Id == id);
            if (ksiazka is null)
            {
                throw new ApplicationException($"Nie znaleziono osoby o ID = {id}");
            }        
            return ksiazka.ToKsiążkaDTO();
        }
        public void Post(KsiążkaPostDTO książkaPostDTO)
        {
            ValidateKsiążkaDTO(książkaPostDTO);
            Książka książka = new()
            {
                Tytul = książkaPostDTO.Tytul,
                Autor = książkaPostDTO.Autor,
                Gatunek = książkaPostDTO.Gatunek,
                Rok = książkaPostDTO.Rok,
                Id = IdGen++
            };
            ksiazkiDbSet.Add(książka);
        }
        public void Delete(int id)
        {
            var ksiazka = ksiazkiDbSet.FirstOrDefault(k => k.Id == id);
            if (ksiazka is null)
                throw new ApplicationException($"Nie znaleziono książki o ID = {id}");

            ksiazkiDbSet.Remove(ksiazka);
        }
        public void Put(int id, KsiążkaPostDTO książkaPostDTO)
        {
            var ksiazka = ksiazkiDbSet.FirstOrDefault(k => k.Id == id);
            if (ksiazka is null)
                throw new ApplicationException($"Nie znaleziono książki o ID = {id}");
            ValidateKsiążkaDTO(książkaPostDTO);
            ksiazka.Tytul = książkaPostDTO.Tytul;
            ksiazka.Autor = książkaPostDTO.Autor;
            ksiazka.Gatunek = książkaPostDTO.Gatunek;
            ksiazka.Rok = książkaPostDTO.Rok;
        }
        public IEnumerable<KsiążkaDTO> GetByTytul(string? fraza)
        {
            return ksiazkiDbSet
            .Where(k => k.Tytul.Contains(fraza, StringComparison.OrdinalIgnoreCase))
            .Select(k => k.ToKsiążkaDTO());
        }
        private void ValidateKsiążkaDTO(KsiążkaPostDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Tytul))
                throw new ArgumentException("Tytuł jest wymagany.");

            if (dto.Tytul.Length > 100)
                throw new ArgumentException("Tytuł może mieć maksymalnie 100 znaków.");

            if (string.IsNullOrWhiteSpace(dto.Gatunek))
                throw new ArgumentException("Gatunek jest wymagany.");

            if (!DozwoloneGatunki.Contains(dto.Gatunek.ToLower()))
                throw new ArgumentException("Gatunek musi być jednym z: fantasy, romans, dystopia.");

            if (dto.Rok > 2025)
                throw new ArgumentException("Rok nie może być większy niż 2025.");
        }
    }
}
