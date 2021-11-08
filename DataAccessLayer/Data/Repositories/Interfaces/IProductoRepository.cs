using ImprentaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.Data.Repositories
{
    public interface IProductoRepository
    {
        public Task<IEnumerable<ProductoModel>> GetProductosAsync(string orderBy = "Id");
        public Task<ProductoModel> GetProductoAsync(long productoId);
        public  Task<ProductoModel> CreateProductAsync(ProductoModel nuevoProducto);
        public Task<ProductoModel> UpdateProductAsync(long productoId,ProductoModel actualizarProducto);
        public Task<Boolean> DeleteProductAsync(long productoId);

    }
}
