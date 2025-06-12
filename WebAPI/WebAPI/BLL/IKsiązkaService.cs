using WebAPI.BLL.ModelsDTO;

namespace WebAPI.BLL
{
    public interface IKsiązkaService
    {
        public IEnumerable<KsiążkaDTO> Get();

        public IEnumerable<KsiążkaDTO> GetByTytul(string? fraza);

        public KsiążkaDTO GetById(int id);
        public void Post(KsiążkaPostDTO książkaPostDTO);

        public void Delete(int id);

        public void Put(int id, KsiążkaPostDTO książkaPostDTO);
    }
}
