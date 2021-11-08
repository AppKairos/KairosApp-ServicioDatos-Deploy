using Dapper;
using ImprentaAPI.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.Data.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private PostgreSQLConfiguration _connection;
        private NpgsqlConnection _baseAbierta;

        protected NpgsqlConnection AbreConexion()
        {
            return  _connection.AbreConexion();
        }
        public ProductoRepository(PostgreSQLConfiguration connectionString)
        {
            _connection = connectionString;
            _baseAbierta= AbreConexion();
        }
        public async Task<ProductoModel> GetProductoAsync(long productoId)
        {
 
            var sql = @"SELECT * FROM public.productos WHERE estado=true and id = "+ productoId.ToString();
            return await _baseAbierta.QueryFirstOrDefaultAsync<ProductoModel>(sql, new ProductoModel { });
        
        }

        public async Task<IEnumerable<ProductoModel>> GetProductosAsync(string orderBy = "Id")
        {
 
            var sql = @"SELECT * FROM public.productos WHERE estado=true ORDER BY id ASC ";
            return await _baseAbierta.QueryAsync<ProductoModel>(sql, new { });
        }

        public async Task<ProductoModel> CreateProductAsync(ProductoModel nuevoProducto)
        {
 
            var sql = @"
                    INSERT INTO public.productos(nombre, descripcion, estado)
	                VALUES (@Nombre, @Descripcion, @Estado)";
            var resultado = await _baseAbierta.ExecuteAsync(sql, new { nuevoProducto.Id, nuevoProducto.Nombre, nuevoProducto.Descripcion, nuevoProducto.Estado});
            var sql2 = @"SELECT * FROM public.productos ORDER BY id DESC LIMIT 1";
            var resultToShow = await _baseAbierta.QueryFirstOrDefaultAsync<ProductoModel>(sql2, new ProductoModel { });
            return resultToShow;    
        }

        public async  Task<ProductoModel> UpdateProductAsync(long productoId, ProductoModel actualizarProducto)
        {
            
            var sql = @"
                    UPDATE public.productos
	                SET nombre=coalesce(@Nombre,nombre), descripcion=coalesce(@Descripcion,descripcion), estado=coalesce(@Estado,estado)
	                WHERE id = @Id";
            var result = await _baseAbierta.ExecuteAsync(sql, new { actualizarProducto.Nombre, actualizarProducto.Descripcion,actualizarProducto.Estado, Id=productoId});
            var resultToShow = await GetProductoAsync(productoId);
            return resultToShow;
        }

        public async Task<bool> DeleteProductAsync(long productoId)
        {
 
            var sql = @"
                    UPDATE public.productos
                    SET estado=false
	                WHERE id = @Id";
            var result = await _baseAbierta.ExecuteAsync(sql,new { Id = productoId});
            return result > 0;

        }
    }
}

