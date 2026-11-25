using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly Interfaces.ICommentRepository _commentRepo;
        public CommentController(Interfaces.ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();

            var commentDto = comments.Select(c => c.TCommentDto());
            return Ok(commentDto);
        }
    }
}