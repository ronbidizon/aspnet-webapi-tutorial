using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

namespace api.Repository
{
    public class CommentRepository : Interfaces.ICommentRepository
    {
        private readonly ApplicationDB_context _context;
        public CommentRepository(ApplicationDB_context context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }
    }
}