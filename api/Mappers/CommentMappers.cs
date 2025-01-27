using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                Created = commentModel.Created,
                StockId = commentModel.StockId
            };
        }
        public static Comment ToCommentFromCreateRequestDto(this CreateCommentRequestDto createModel)
        {
            return new Comment
            {
                Title = createModel.Title,
                Content = createModel.Content,
                StockId = createModel.StockId,
            };
        }
        public static Comment ToCommentFromUpdateRequestDto(this UpdateCommentRequestDto createModel)
        {
            return new Comment
            {
                Title = createModel.Title,
                Content = createModel.Content,
                StockId = createModel.StockId,
            };
        }


    }
}