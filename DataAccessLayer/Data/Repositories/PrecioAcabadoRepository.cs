using Dapper;
using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.BusinessLogicLayer.Models.Security;
using ImprentaAPI.Data;
using ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.DataAccessLayer.Data.Repositories
{
    public class PrecioAcabadoRepository : IPrecioAcabadoRepository
    {
        private PostgreSQLConfiguration _connection;
        private NpgsqlConnection _baseAbierta;

        protected NpgsqlConnection AbreConexion()
        {
            return _connection.AbreConexion();
        }
        public PrecioAcabadoRepository(PostgreSQLConfiguration connectionString)
        {
            _connection = connectionString;
            _baseAbierta = AbreConexion();
        }
        public async Task<PrecioAcabadoModel> CreatePrecioAcabadoAsync(PrecioAcabadoModel nuevoPrecio)
        {
            var sql = @"
                    INSERT INTO public.precioacabado(nombreproducto, precio, estado)
	                VALUES (@nombreproducto, @precio, @estado)";
            var resultado = await _baseAbierta.ExecuteAsync(sql, new { nuevoPrecio.NombreProducto, nuevoPrecio.Precio, nuevoPrecio.Estado});
            var sql2 = @"SELECT * FROM public.precioacabado Where estado=true ORDER BY id DESC LIMIT 1";
            var resultToShow = await _baseAbierta.QueryFirstOrDefaultAsync<PrecioAcabadoModel>(sql2, new PrecioAcabadoModel { });
            return resultToShow;
        }

        public async Task<bool> DeletePrecioAcabadoAsync(string nombre)
        {
            var sql = @"
                    UPDATE public.precioacabado
                    SET estado=false
                    WHERE nombreProducto = @Id";
            var result = await _baseAbierta.ExecuteAsync(sql, new { Id = nombre });
            return result > 0;
        }

        public async Task<PrecioAcabadoModel> GetPrecioAcabadoAsync(string nombre)
        {
            var sql = @"SELECT * FROM public.precioacabado WHERE nombreproducto= '" + nombre.ToString() +"' and estado=true";
            return await _baseAbierta.QueryFirstOrDefaultAsync<PrecioAcabadoModel>(sql, new PrecioAcabadoModel { });
        }

        public async Task<IEnumerable<PrecioAcabadoModel>> GetPreciosAcabadoAsync(string nombre)
        {
            var sql = @"SELECT * FROM public.precioacabado WHERE estado=true ORDER BY id ASC";
            return await _baseAbierta.QueryAsync<PrecioAcabadoModel>(sql, new { });
        }

        public async Task<PrecioAcabadoModel> UpdatePrecioAcabadoAsync(string nombre, PrecioAcabadoModel actualizarPrecio)
        {
            var sql = @"
                    UPDATE public.precioacabado
	                SET nombreproducto=coalesce(@nombreproducto,nombreproducto), precio=coalesce(@precio,precio), estado=coalesce(@estado,estado)
	                WHERE nombreproducto = @nombreProducto";
            var result = await _baseAbierta.ExecuteAsync(sql, new { actualizarPrecio.NombreProducto, actualizarPrecio.Precio, actualizarPrecio.Estado, nombreProducto = nombre });
            var resultToShow = await GetPrecioAcabadoAsync(nombre);
            return resultToShow;
        }
    }
}
