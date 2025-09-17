using BookAPIStore.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_simple.Repositories;

namespace BookAPIStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepo;

        public PublishersController(IPublisherRepository publisherRepo)
        {
            _publisherRepo = publisherRepo;
        }

        // GET: api/publishers/getAllPublisher
        [HttpGet("getAllPublisher")]
        public IActionResult GetAllPublisher()
        {
            var result = _publisherRepo.GetAllPublishers();
            return Ok(result);
        }

        // GET: api/publishers/getPublisherById/5
        [HttpGet("getPublisherById/{id:int}")]
        public IActionResult GetPublisherById([FromRoute] int id)
        {
            var result = _publisherRepo.GetPublisherById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/publishers/addPublisher
        [HttpPost("addPublisher")]
        public IActionResult AddPublisher([FromBody] AddPublisherRequestDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = _publisherRepo.AddPublisher(request);
            // Vì method AddPublisher trong repo trả về AddPublisherRequestDTO theo PDF,
            // ta trả Ok hoặc Created theo nhu cầu. Ở đây dùng Ok cho sát chữ ký.
            return Ok(created);
        }

        // PUT: api/publishers/updatePublisherById/5
        [HttpPut("updatePublisherById/{id:int}")]
        public IActionResult UpdatePublisherById([FromRoute] int id, [FromBody] PublisherNoIdDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = _publisherRepo.UpdatePublisherById(id, request);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE: api/publishers/deletePublisherById/5
        [HttpDelete("deletePublisherById/{id:int}")]
        public IActionResult DeletePublisherById([FromRoute] int id)
        {
            var deleted = _publisherRepo.DeletePublisherById(id);
            if (deleted == null) return NotFound();
            return NoContent();
        }
    }
}
