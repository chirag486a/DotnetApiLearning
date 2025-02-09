using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces.Repositories;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {

        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            var comments = await _commentRepo.GetAllAsync();
            if (comments.Count == 0)
            {
                return NoContent();
            }

            return Ok(comments);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            var comment = await _commentRepo.GetAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        [HttpPost]
        [Route("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            if (!await _stockRepo.ExistsAsync(stockId))
            {
                return BadRequest("Invalid stockId.");
            }
            CommentDto comment = await _commentRepo.CreateAsync(stockId, commentDto);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            CommentDto? commentModel = await _commentRepo.DeleteAsync(id);
            if (commentModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            CommentDto? commentModel = await _commentRepo.UpdateAsync(id, commentDto);

            if (commentModel == null)
            {
                return NotFound();
            }
            return Ok(commentModel);
        }

    }
}