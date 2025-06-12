using Microsoft.AspNetCore.Mvc;
using WebAPI.BLL.ModelsDTO;
using WebAPI.BLL;
using WebAPI.BLL_Memory;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KsiążkaController : Controller
    {
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
            _ksiązkaService.Post(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto }, null);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] KsiążkaPostDTO dto)
        {
            try
            {
                _ksiązkaService.Put(id, dto);
                return NoContent();
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
        [HttpGet("szukaj")]
        public ActionResult<IEnumerable<KsiążkaDTO>> GetByTytul([FromQuery] string? fraza)
        {
            return Ok(_ksiązkaService.GetByTytul(fraza));
        }
    }
}
