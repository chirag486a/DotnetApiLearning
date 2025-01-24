using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    // [Route("api/[controller]")]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Stock> stocks = _context.Stocks.ToList();
            if (stocks == null)
            {
                return NotFound();
            }
            return Ok(stocks.Select(s => s.ToStockDto()));
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            Stock? stock = _context.Stocks.Find(id);

            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            Stock stockModel = stockDto.ToStockFromCreateRequestDto();
            if (stockModel == null)
            {
                return BadRequest("Product is Null");
            }
            _context.Add(stockModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            Stock? stockModel = _context.Stocks.FirstOrDefault(stock => stock.Id == id);

            if (stockModel == null)
            {
                return NotFound();
            }
            ;

            Stock updates = updateDto.ToStockFromUpdateRequestDto();

            stockModel.Symbol = updates.Symbol;
            stockModel.CompanyName = updates.CompanyName;
            stockModel.Price = updates.Price;
            stockModel.LastDiv = updates.LastDiv;
            stockModel.Industry = updates.Industry;
            stockModel.MarketCap = updates.MarketCap;

            _context.SaveChanges();

            return Ok(stockModel.ToStockDto());
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            Stock? stock = _context.Stocks.FirstOrDefault(s => s.Id == id);

            if (stock == null)
            {
                return Ok();
            }

            _context.Stocks.Remove(stock);
            _context.SaveChanges();

            return Ok();
        }



    }
}