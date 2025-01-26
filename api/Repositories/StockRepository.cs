using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces.Repositories;
using api.Mappers;
using api.Migrations;
using api.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<List<StockDto>> GetAllAsync()
        {
            List<Stock> stocks = await _context.Stocks.ToListAsync();

            return [.. stocks.Select(s => s.ToStockDto())];
        }
        public async Task<StockDto?> GetAsync(int id)
        {
            Stock? stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            return stockModel.ToStockDto();
        }
        public async Task<StockDto> CreateAsync(CreateStockRequestDto createStock)
        {
            Stock stockModel = createStock.ToStockFromCreateRequestDto();

            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            return stockModel.ToStockDto();
        }
        public async Task<StockDto?> UpdateAsync(int id, UpdateStockRequestDto updates)
        {
            var stockModel = updates.ToStockFromUpdateRequestDto();
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);


            if (existingStock == null)
            {
                return null;
            }

            existingStock.Symbol = stockModel.Symbol;
            existingStock.CompanyName = stockModel.CompanyName;
            existingStock.Price = stockModel.Price;
            existingStock.LastDiv = stockModel.LastDiv;
            existingStock.Industry = stockModel.Industry;
            existingStock.MarketCap = stockModel.MarketCap;

            await _context.SaveChangesAsync();

            return existingStock.ToStockDto();
        }

        public async Task<StockDto?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel.ToStockDto();
        }

    }
}