using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Interfaces.Repositories
{
    public interface IStockRepository
    {
        public Task<List<StockDto>> GetAllAsync();
        public Task<StockDto?> GetAsync(int id);
        public Task<StockDto> CreateAsync(CreateStockRequestDto createStock);
        public Task<StockDto?> UpdateAsync(int id, UpdateStockRequestDto updates);
        public Task<StockDto?> DeleteAsync(int id);

    }
}