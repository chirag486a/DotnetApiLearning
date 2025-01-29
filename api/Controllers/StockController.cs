using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers.QueryObj;
using api.Interfaces.Repositories;
using api.Mappers;
using api.Models;
using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    // [Route("api/[controller]")]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(IStockRepository stockRepo, ApplicationDBContext context)
        {
            _context = context;
            _stockRepo = stockRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] StockGetAllQueryObj queryObj)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            List<StockDto> stocks = await _stockRepo.GetAllAsync(queryObj);
            if (stocks == null)
            {
                return NotFound();
            }
            return Ok(stocks);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            StockDto? stock = await _stockRepo.GetAsync(id);

            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            StockDto stockModel = await _stockRepo.CreateAsync(stockDto);
            if (stockModel == null)
            {
                return BadRequest("Product is Null");
            }
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            StockDto? stockModel = await _stockRepo.UpdateAsync(id, updateDto);

            if (stockModel == null)
            {
                return NotFound();
            }
            return Ok(stockModel);
        }


        [HttpDelete]
        [Route("{id:int}")]
        // FIX: Delete all comments before deleting stock.
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            StockDto? stock = await _stockRepo.DeleteAsync(id);

            if (stock == null)
            {
                return NoContent();
            }

            return NoContent();
        }
    }
}