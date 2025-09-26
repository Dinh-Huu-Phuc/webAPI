using Microsoft.AspNetCore.Mvc;
using BookAPIStore.Repositories;
using BookAPIStore.Models.DTO;
using WebAPI.Repositories;
using WebAPI.Models.DTO;

namespace BookAPIStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepo;

        public AuthorsController(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }

        // GET: api/authors/getAllAuthor
        [HttpGet("getAllAuthor")]
        public IActionResult GetAllAuthor()
        {
            var result = _authorRepo.GellAllAuthors();
            return Ok(result);
        }

        // GET: api/authors/getAuthorById/5
        [HttpGet("getAuthorById/{id:int}")]
        public IActionResult GetAuthorById([FromRoute] int id)
        {
            var result = _authorRepo.GetAuthorById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/authors/addAuthor
        [HttpPost("addAuthor")]
        public IActionResult AddAuthor([FromBody] AddAuthorRequestDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = _authorRepo.AddAuthor(request);
            return Ok(created);
        }

        // PUT: api/authors/updateAuthorById/5
        [HttpPut("updateAuthorById/{id:int}")]
        public IActionResult UpdateAuthorById([FromRoute] int id, [FromBody] AuthorNoIdDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = _authorRepo.UpdateAuthorById(id, request);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE: api/authors/deleteAuthorById/5
        [HttpDelete("deleteAuthorById/{id:int}")]
        public IActionResult DeleteAuthorById([FromRoute] int id)
        {
            // BÀI 8: nếu còn Book_Authors → báo lỗi
            if (_authorRepo.HasAnyBook(id))
            {
                return BadRequest(new
                {
                    error = "Không thể xóa Author vì vẫn còn sách liên kết qua Book_Author.",
                    suggestion = "Hãy gỡ liên kết trong Book_Author trước khi xóa."
                });
            }

            var deleted = _authorRepo.DeleteAuthorById(id);
            if (deleted == null) return NotFound();
            return NoContent();
        }
    }
}
