using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        public Task<List<CommentDto>> GetAllAsync();
        public Task<CommentDto?> GetAsync(int id);
        public Task<CommentDto> CreateAsync(int stockId, CreateCommentRequestDto commentDto);
        public Task<CommentDto?> UpdateAsync(int id, UpdateCommentRequestDto commentDto);
        public Task<CommentDto?> DeleteAsync(int id);
    }
}