using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static Dtos.Comment.CommentDto TCommentDto(this Comment commentModel)
        {
            return new Dtos.Comment.CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }
    }
}