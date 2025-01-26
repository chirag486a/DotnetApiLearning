using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces.Repositories;
using api.Mappers;
using api.Models;
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
    }
}