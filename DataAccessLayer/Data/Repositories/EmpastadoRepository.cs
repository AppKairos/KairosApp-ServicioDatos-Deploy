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
    public class EmpastadoRepository : IEmpastadoRepository
    {
        private PostgreSQLConfiguration _connection;
        private NpgsqlConnection _baseAbierta;

        protected NpgsqlConnection AbreConexion()
        {
            return _connection.AbreConexion();
        }
        public EmpastadoRepository(PostgreSQLConfiguration connectionString)
        {
            _connection = connectionString;
            _baseAbierta = AbreConexion();
        }

        public async Task<EmpastadoModel> CreateEmpastadoAsync(EmpastadoModel nuevoEmpasatado)
        {
            var sql = @"INSERT INTO public.empastados(facultad,carrera,titulotesis,trabajo,autor,tutor,mes,anio,cantidad,idusuario,tipousuario,estado)
                            VALUES (@Facultad,@Carrera,@TituloTesis,@Trabajo,@Autor,@Tutor,@Mes,@Anio,@Cantidad,@IdUsuario,@TipoUsuario,@Estado)";
            var resultado = await _baseAbierta.ExecuteAsync(sql, new { nuevoEmpasatado.Id, nuevoEmpasatado.Facultad, nuevoEmpasatado.Carrera, nuevoEmpasatado.TituloTesis, nuevoEmpasatado.Trabajo, nuevoEmpasatado.Autor, nuevoEmpasatado.Tutor, nuevoEmpasatado.Mes, nuevoEmpasatado.Anio, nuevoEmpasatado.Cantidad, nuevoEmpasatado.IdUsuario, nuevoEmpasatado.TipoUsuario, nuevoEmpasatado.Estado});
            var sql2 = @"Select * from public.empastados ORDER BY id DESC LIMIT 1";
            var resultadoToShow = await _baseAbierta.QueryFirstOrDefaultAsync<EmpastadoModel>(sql2, new EmpastadoModel { });
            var IdItem = resultadoToShow.Id;
            var Nombre = "Empastado";
            var IdUsuario = resultadoToShow.IdUsuario;
            var TipoUsuario = resultadoToShow.TipoUsuario;
            var Estado = resultadoToShow.Estado;
            var sql3 = @"INSERT INTO public.reservas(iditem,idusuario,nombre,tipousuario,estado) VALUES(@IdItem,@IdUsuario,@Nombre,@TipoUsuario,@Estado)";
            var resultado2 = await _baseAbierta.ExecuteAsync(sql3, new { IdItem, IdUsuario, Nombre, TipoUsuario, Estado });
            return resultadoToShow;
        }

        public async Task<bool> DeleteEmpastadoAsync(long empastadoId)
        {
            var sql = @"UPDATE  public.empastados SET estado=false WHERE id=@Id";
            var result = await _baseAbierta.ExecuteAsync(sql, new { Id = empastadoId });
            var sql1 = @"UPDATE  public.reservas SET estado=false WHERE iditem=@Id";
            var result2 = await _baseAbierta.ExecuteAsync(sql1, new { Id = empastadoId });
            return result>0;
        }

        public async Task<EmpastadoModel> GetEmpastadoAsync(long empastadoId)
        {
            var sql = @"SELECT * FROM public.empastados WHERE estado=true and id = " + empastadoId.ToString();
            return await _baseAbierta.QueryFirstOrDefaultAsync<EmpastadoModel>(sql, new EmpastadoModel { });
        }

        public async Task<IEnumerable<EmpastadoModel>> GetEmpastadosAsync(string orderBy = "Id")
        {
            var sql = @"SELECT * FROM public.empastados WHERE estado=true ORDER BY id ASC";
            return await _baseAbierta.QueryAsync<EmpastadoModel>(sql, new { });
        }

        public async Task<EmpastadoModel> UpdateEmpastadoAsync(long empastadoId, EmpastadoModel actualizarEmpastado)
        {
            var sql = @"UPDATE public.empastados SET facultad=coalesce(@Facultad,facultad),carrera=coalesce(@Carrera,carrera),titulotesis=coalesce(@TituloTesis,titulotesis),
                        trabajo=coalesce(@Trabajo,trabajo),autor=coalesce(@Autor,autor),tutor=coalesce(@Tutor,tutor),mes=coalesce(@Mes,mes),
                        anio=coalesce(@Anio,anio),cantidad=coalesce(@Cantidad,cantidad),idusuario=coalesce(@IdUsuario,idusuario),tipousuario=coalesce(@TipoUsuario,tipousuario),
                        estado=coalesce(@Estado,estado)
                        WHERE id=@Id";
            var result = await _baseAbierta.ExecuteAsync(sql, new
            {
                actualizarEmpastado.Facultad,
                actualizarEmpastado.Carrera,
                actualizarEmpastado.TituloTesis,
                actualizarEmpastado.Trabajo,
                actualizarEmpastado.Autor,
                actualizarEmpastado.Tutor,
                actualizarEmpastado.Mes,
                actualizarEmpastado.Anio,
                actualizarEmpastado.Cantidad,
                actualizarEmpastado.IdUsuario,
                actualizarEmpastado.TipoUsuario,
                actualizarEmpastado.Estado,
                Id = empastadoId
            });
            var resultToShow = await GetEmpastadoAsync(empastadoId);
            return resultToShow;
        }

        public Task<IEnumerable<ReservaModel>> GetReservasTipoUsuarioAsync(string tipo)
        {
            throw new NotImplementedException();
        }
    }
}
