using BookAPIStore.Models.DTO;
using BookAPIStore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.DTO;
using WebAPI.Repositories;

namespace BookAPIStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        // GET: api/publishers/getPublisherById/{id}
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check trùng tên
            if (_publisherRepo.ExistsByName(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), "Publisher name already exists.");
                return BadRequest(ModelState);
                // return Conflict(new { error = "Publisher name already exists." });
            }

            var created = _publisherRepo.AddPublisher(request);
            return Ok(created);
        }

        // PUT: api/publishers/updatePublisherById/{id}
        [HttpPut("updatePublisherById/{id:int}")]
        public IActionResult UpdatePublisherById([FromRoute] int id, [FromBody] PublisherNoIdDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check trùng tên (trừ chính nó ra)
            if (_publisherRepo.ExistsByNameExcludingId(request.Name, id))
            {
                ModelState.AddModelError(nameof(request.Name), "Publisher name already exists.");
                return BadRequest(ModelState);
                // return Conflict(new { error = "Publisher name already exists." });
            }

            var updated = _publisherRepo.UpdatePublisherById(id, request);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE: api/publishers/deletePublisherById/{id}
        [HttpDelete("deletePublisherById/{id:int}")]
        public IActionResult DeletePublisherById([FromRoute] int id)
        {
            // BÀI 7: kiểm tra còn Books tham chiếu không
            if (_publisherRepo.HasBooks(id))
            {
                return BadRequest(new
                {
                    error = "Không thể xóa Publisher vì đang có Book tham chiếu.",
                    suggestion = "Hãy xóa/chuyển sách sang Publisher khác hoặc cấu hình cascade."
                });
            }

            var deleted = _publisherRepo.DeletePublisherById(id);
            if (deleted == null) return NotFound();
            return NoContent();
        }
    }
}
