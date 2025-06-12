using Microsoft.AspNetCore.Mvc;
using WebAPI.BLL.ModelsDTO;
using WebAPI.BLL;
using WebAPI.BLL_Memory;
using WebAPI.DAL.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KsiążkaController : Controller
    {
        private static readonly string[] DozwoloneGatunki = { "fantasy", "romans", "dystopia" };

        private readonly IKsiązkaService _ksiązkaService;

        public KsiążkaController(IKsiązkaService ksiązkaService)
        {
            _ksiązkaService = ksiązkaService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<KsiążkaDTO>> Get()
        {
            return Ok(_ksiązkaService.Get());
        }

        [HttpGet("{id}")]
        public ActionResult<KsiążkaDTO> GetById(int id)
        {
            try
            {
                return Ok(_ksiązkaService.GetById(id));
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] KsiążkaPostDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!DozwoloneGatunki.Contains(dto.Gatunek.ToLower()))
                return BadRequest("Gatunek musi być jednym z: fantasy, romans, dystopia.");

            _ksiązkaService.Post(dto);
            return Ok(dto);
        }
            [HttpPut("{id}")]
            public IActionResult Put(int id, [FromBody] KsiążkaPostDTO dto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!DozwoloneGatunki.Contains(dto.Gatunek.ToLower()))
                    return BadRequest("Gatunek musi być jednym z: fantasy, romans, dystopia.");

                _ksiązkaService.Put(id, dto);
                return Ok(dto);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _ksiązkaService.Delete(id);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{tytul}")]
        public ActionResult<IEnumerable<KsiążkaDTO>> GetByTytul([FromQuery] string? fraza)
        {
            return Ok(_ksiązkaService.GetByTytul(fraza));
        }
    }
}
