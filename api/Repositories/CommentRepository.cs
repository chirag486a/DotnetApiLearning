using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces.Repositories;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<CommentDto>> GetAllAsync()
        {
            var comments = await _context.Comments.ToListAsync();
            return comments.Select(c => c.ToCommentDto()).ToList();
        }
        public async Task<CommentDto?> GetAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null) return null;
            return comment.ToCommentDto();
        }
        public async Task<CommentDto> CreateAsync(CreateCommentRequestDto commentDto)
        {
            Comment comment = commentDto.ToCommentFromCreateRequestDto();
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment.ToCommentDto();
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<CommentDto?> UpdateAsync(int id, UpdateCommentRequestDto commentDto)
        {
            Comment? existingModel = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (existingModel == null)
            {
                return null;
            }

            Comment commentModel = commentDto.ToCommentFromUpdateRequestDto();

            existingModel.Content = commentModel.Content;
            existingModel.Title = commentModel.Title;
            existingModel.StockId = commentModel.StockId;

            await _context.SaveChangesAsync();

            return existingModel.ToCommentDto();
        }
        public async Task<CommentDto?> DeleteAsync(int id)
        {
            Comment? commentModel = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (commentModel == null) return null;

            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel.ToCommentDto();

        }
    }
}