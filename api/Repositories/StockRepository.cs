using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers.QueryObj;
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


        public async Task<List<StockDto>> GetAllAsync(StockGetAllQueryObj queryObj)
        {
            IQueryable<Stock> stocks = _context.Stocks.Include(s => s.Comments).AsQueryable();
            int SkipNumber = Convert.ToInt32((queryObj.PageNumber - 1) * queryObj.PageSize);
            int TakeNumber = Convert.ToInt32(queryObj.PageSize);
            stocks = stocks.Skip(SkipNumber).Take(TakeNumber);

            if (!string.IsNullOrWhiteSpace(queryObj.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(queryObj.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(queryObj.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(queryObj.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(queryObj.SortBy))
            {
                if (queryObj.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObj.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }

                if (queryObj.SortBy.Equals("MarketCap", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObj.IsDecsending ? stocks.OrderByDescending(s => s.MarketCap) : stocks.OrderBy(s => s.MarketCap);
                }
                if (queryObj.SortBy.Equals("Purchase", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObj.IsDecsending ? stocks.OrderByDescending(s => s.Purchase) : stocks.OrderBy(s => s.Purchase);
                }
            }

            return [.. (await stocks.ToListAsync()).Select(s => s.ToStockDto())];
        }
        public async Task<StockDto?> GetAsync(int id)
        {
            Stock? stockModel = await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
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
            existingStock.Purchase = stockModel.Purchase;
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
        public async Task<Boolean> ExistsAsync(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }


    }
}