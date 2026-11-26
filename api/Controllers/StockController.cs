using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly api.Data.ApplicationDB_context _context;
        private readonly Interfaces.IStockRepository _stcokRepo;
        public StockController(api.Data.ApplicationDB_context context, Interfaces.IStockRepository stockRepo)
        {
            _stcokRepo = stockRepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Helpers.QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stcokRepo.GetAllAsync(query);
            
            var stockDto = stocks.Select(s => s.ToStockDto());
            
            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stcokRepo.GetByIdAsync(id);
            
            if (stock == null)
            {
                return NotFound();
            }
            
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = stockDto.ToStockFromCreateDTO();
            
            await _stcokRepo.CreateAsync(stock);
            
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stcokRepo.UpdateAsync(id, updateDto);
            
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stcokRepo.DeleteAsync(id);
            
            if (stock == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}