using WebAPI.BLL.ModelsDTO;
using WebAPI.DAL.Models;

namespace WebAPI.BLL_Memory.Extensions
{
    internal static class KsiążkaEntity
    {
        public static KsiążkaDTO ToKsiążkaDTO(this Książka książkaEntity)
        {
            return new KsiążkaDTO(książkaEntity.Id, książkaEntity.Tytul, książkaEntity.Autor, książkaEntity.Gatunek, książkaEntity.Rok);
        }
    }
}
