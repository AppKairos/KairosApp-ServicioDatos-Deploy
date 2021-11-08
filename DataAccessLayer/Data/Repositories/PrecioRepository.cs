using Dapper;
using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.Data;
using ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.DataAccessLayer.Data.Repositories
{
    public class PrecioRepository : IPrecioRepository
    {
        private PostgreSQLConfiguration _connection;
        private NpgsqlConnection _baseAbierta;

        protected NpgsqlConnection AbreConexion()
        {
            return _connection.AbreConexion();
        }
        public PrecioRepository(PostgreSQLConfiguration connectionString)
        { 
            _connection = connectionString;
            _baseAbierta = AbreConexion();
        }
        public async Task<PrecioModel> CreatePrecioAsync(PrecioModel nuevaPrecio)
        {
            var precioNeto= nuevaPrecio.precioresma/nuevaPrecio.pliegos;
             var sql = @"
                    INSERT INTO public.precios(nombre, precioneto,tipo,estado,tam,precioresma,pliegos)
	                VALUES (@nombre, @precioneto,@tipo,true,@tam,@precioresma,@pliegos)";
            var resultado = await _baseAbierta.ExecuteAsync(sql, new { nuevaPrecio.nombre, precioNeto, nuevaPrecio.tipo,nuevaPrecio.tam,nuevaPrecio.precioresma,nuevaPrecio.pliegos});
            var sql2 = @"SELECT * FROM public.precios Where estado=true ORDER BY id DESC LIMIT 1";
            var resultToShow = await _baseAbierta.QueryFirstOrDefaultAsync<PrecioModel>(sql2, new PrecioModel { });
            return resultToShow;
        }

        public async Task<bool> DeletePrecioAsync(long precioId)
        {
            var sql = @"
                    UPDATE public.precios
                    SET estado=false
                    WHERE id = @Id";
            var result = await _baseAbierta.ExecuteAsync(sql, new { Id = precioId });
            return result > 0;
        }

        public async Task<PrecioModel> GetPrecioAsync(long precioId)
        {
            var sql = @"SELECT * FROM public.precios WHERE id = " + precioId.ToString();
            return await _baseAbierta.QueryFirstOrDefaultAsync<PrecioModel>(sql, new PrecioModel { });
        }

        public async Task<IEnumerable<PrecioModel>> GetPreciosAsync(string nombre)
        {
            string sql;
            if (nombre=="")
                 sql = @"SELECT * FROM public.precios Where estado=true ORDER BY id ASC";
            else
                 sql = @"SELECT * FROM public.precios Where estado=true and nombre='"+nombre+"' ORDER BY id ASC";
            
            return await _baseAbierta.QueryAsync<PrecioModel>(sql, new { });
        }
        public async Task<IEnumerable<string>> GetPreciosNombreAsync()
        { 
            var sql = @"SELECT * FROM public.precios Where estado=true ORDER BY id ASC";
            var res = await _baseAbierta.QueryAsync<PrecioModel>(sql, new { });
            List<string> nombres = new List<string>();
            foreach(var i in res)
            {
                nombres.Add(i.nombre);
            }           
            return nombres.Distinct().ToList();
        }
        public async Task<IEnumerable<string>> GetPreciosTipoxNombreAsync(string nombre)
        {
            var sql = @"SELECT * FROM public.precios WHERE nombre='"+nombre+"' and estado=true";
            var res = await _baseAbierta.QueryAsync<PrecioModel>(sql, new { });
            List<string> tipos = new List<string>();
            foreach (var i in res.OrderByDescending(e => e.tipo))
            {
                tipos.Add(i.tipo);
            }
            return tipos.ToList();
        }
        public async Task<PrecioModel> UpdatePrecioAsync(long precioId, PrecioModel actualizarPrecio)
        {
            var sql = @"
                        UPDATE public.precios
	                    SET nombre=coalesce(@nombre,nombre), precioneto=round(CAST(float8 (coalesce(@precioresma,precioresma)/coalesce(@pliegos,pliegos)) as numeric ),2), tipo=coalesce(@tipo,tipo),tam=coalesce(@tam,tam),precioresma=coalesce(@precioresma,precioresma),pliegos=coalesce(@pliegos,pliegos)
	                    WHERE id = @Id";
            var result = await _baseAbierta.ExecuteAsync(sql, new { actualizarPrecio.nombre, actualizarPrecio.precioneto,actualizarPrecio.tipo, Id=precioId, actualizarPrecio.tam,actualizarPrecio.precioresma,actualizarPrecio.pliegos});
            var resultToShow = await GetPrecioAsync(precioId);
            return resultToShow;
        }
    }
}
