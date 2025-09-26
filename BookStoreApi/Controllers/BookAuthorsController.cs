using BookAPIStore.Models.DTO;
using BookAPIStore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPIStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        private readonly IBookAuthorRepository _repo;
        public BookAuthorsController(IBookAuthorRepository repo) => _repo = repo;

        // POST: api/bookauthors/add
        [HttpPost("add")]
        public IActionResult Add([FromBody] AddBookAuthorRequestDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // BÀI 5: kiểm tra BookId & AuthorId có tồn tại
            if (!_repo.BookExists(request.BookId))
            {
                ModelState.AddModelError(nameof(request.BookId), "BookId không tồn tại.");
                return BadRequest(ModelState);
            }
            if (!_repo.AuthorExists(request.AuthorId))
            {
                ModelState.AddModelError(nameof(request.AuthorId), "AuthorId không tồn tại.");
                return BadRequest(ModelState);
            }

           
            if (_repo.RelationExists(request.BookId, request.AuthorId))
            {
                return Conflict(new
                {
                    error = "Quan hệ (BookId, AuthorId) đã tồn tại.",
                    detail = new { request.BookId, request.AuthorId }
                });
            }

            var created = _repo.AddRelation(request);
            return Ok(new { created.Id, created.BookId, created.AuthorId });
        }
    }
}
