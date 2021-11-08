using ImprentaAPI.Data.Repositories;
using ImprentaAPI.Exceptions;
using ImprentaAPI.Models;
using ImprentaAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<ProductoModel> CreateProductAsync(ProductoModel newProducto)
        {
            var nuevoProducto = await _productoRepository.CreateProductAsync(newProducto);
            if (nuevoProducto!=null)
            {
                return nuevoProducto;
            }
            throw new Exception("No se pudo crear el producto");
        }

        public async Task<bool> DeleteProductAsync(long productoId)
        {
            var productoBorrado = await _productoRepository.DeleteProductAsync(productoId);
            if (productoBorrado)
            {
                return true;
            }
            throw new Exception("No se pudo eliminar el producto, revise su existencia");

        }

        public async Task<ProductoModel> GetProductoAsync(long productoId)
        {
            var producto = await _productoRepository.GetProductoAsync(productoId);
            if (producto==null)
            {
                throw new NotFoundProductException($"El producto con id {productoId} no existe");
            }
            return producto;
        }

        public async Task<IEnumerable<ProductoModel>> GetProductosAsync(string orderBy = "Id")
        {
            var productosLista = await _productoRepository.GetProductosAsync(orderBy.ToLower());
            return productosLista;
        }

        public async Task<ProductoModel> UpdateProductAsync(long productoId, ProductoModel actualizarProducto)
        {
            var resultado= await _productoRepository.UpdateProductAsync(productoId,actualizarProducto);
            if (resultado==null)
            {
                throw new Exception("No se pudo actualizar el producto, revise su existencia");
            }
            return resultado;
        }
    }
}

