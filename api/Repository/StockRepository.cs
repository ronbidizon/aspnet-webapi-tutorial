using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : Interfaces.IStockRepository
    {
        private readonly api.Data.ApplicationDB_context _context;
        public StockRepository(api.Data.ApplicationDB_context context)
        {
            _context = context;
        }

       public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stocks.ToListAsync();
        }
    }
}