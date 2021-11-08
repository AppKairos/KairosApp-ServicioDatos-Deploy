using Dapper;
using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.DataAccessLayer.Data.Repositories
{
    public class SelloRepository : ISelloRepository
    {
        private PostgreSQLConfiguration _connection;
        private NpgsqlConnection _baseAbierta;

        protected NpgsqlConnection AbreConexion()
        {
            return _connection.AbreConexion();
        }
        public SelloRepository(PostgreSQLConfiguration connectionString)
        {
            _connection = connectionString;
            _baseAbierta = AbreConexion();
        }
        public async Task<SelloModel> GetSelloAsync(long selloId)
        {

            var sql = @"SELECT * FROM public.sellos WHERE estado=true and id = " + selloId.ToString();
            return await _baseAbierta.QueryFirstOrDefaultAsync<SelloModel>(sql, new SelloModel { });

        }

        public async Task<IEnumerable<SelloModel>> GetSellosAsync(string orderBy = "Id")
        {

            var sql = @"SELECT * FROM public.sellos WHERE estado=true ORDER BY id ASC";
            return await _baseAbierta.QueryAsync<SelloModel>(sql, new { });
        }

        public async Task<SelloModel> CreateSelloAsync(SelloModel nuevoSello)
        {

            var sql = @"
                    INSERT INTO public.sellos(nombre, tipo, maquinas, preciomaquina, preciototal,estado)
	                VALUES (@Nombre, @Tipo, @Maquinas, @PrecioMaquina, @PrecioTotal, @Estado)";
            var resultado = await _baseAbierta.ExecuteAsync(sql, new { nuevoSello.Id, nuevoSello.Nombre, nuevoSello.Tipo, nuevoSello.Maquinas, nuevoSello.PrecioMaquina, nuevoSello.PrecioTotal, nuevoSello.Estado });
            var sql2 = @"SELECT * FROM public.sellos ORDER BY id DESC LIMIT 1";
            var resultToShow = await _baseAbierta.QueryFirstOrDefaultAsync<SelloModel>(sql2, new SelloModel { });
            return resultToShow;
        }

        public async Task<SelloModel> UpdateSelloAsync(long selloId, SelloModel actualizarSello)
        {
            var sql = @"
                    UPDATE public.sellos
	                SET nombre=coalesce(@Nombre,nombre), tipo=coalesce(@Tipo,tipo), maquinas=coalesce(@Maquinas,maquinas), preciomaquina=coalesce(@PrecioMaquina,preciomaquina), preciototal=coalesce(@PrecioTotal,preciototal), estado=coalesce(@Estado,estado)
	                WHERE id = @Id";
            var result = await _baseAbierta.ExecuteAsync(sql, new { actualizarSello.Nombre, actualizarSello.Tipo, actualizarSello.Maquinas, actualizarSello.PrecioMaquina, actualizarSello.PrecioTotal, actualizarSello.Estado, Id=selloId });
            var resultToShow = await GetSelloAsync(selloId);
            return resultToShow;
        }

        public async Task<bool> DeleteSelloAsync(long selloId)
        {

            var sql = @"
                    UPDATE public.sellos
                    SET estado=false
	                WHERE id = @Id";
            var result = await _baseAbierta.ExecuteAsync(sql, new { Id = selloId });
            return result > 0;

        }
    }
}

