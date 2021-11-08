using Dapper;
using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.Data;
using ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces;
using ImprentaAPI.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.DataAccessLayer.Data.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private PostgreSQLConfiguration _connection;
        private NpgsqlConnection _baseAbierta;

        protected NpgsqlConnection AbreConexion()
        {
            return _connection.AbreConexion();
        }
        public ReservaRepository(PostgreSQLConfiguration connectionString)
        {
            _connection = connectionString;
            _baseAbierta = AbreConexion();
        } 
        public async Task<ReservaModel> CreateReservaAsync(ReservaModel nuevaReserva)
        {
            var sql = @"
                    INSERT INTO public.reservas(iditem, idusuario, nombre, tipousuario)
	                VALUES (@IdItem, @IdUsuario, @Nombre, @TipoUsuario)";
            var resultado = await _baseAbierta.ExecuteAsync(sql,  new {nuevaReserva.IdItem, nuevaReserva.IdUsuario, nuevaReserva.Nombre,nuevaReserva.TipoUsuario } );
            var sql2 = @"SELECT * FROM public.reservas ORDER BY id DESC LIMIT 1";
            var resultToShow = await _baseAbierta.QueryFirstOrDefaultAsync<ReservaModel>(sql2, new ReservaModel { });
            return resultToShow;
        }

        public async Task<bool> DeleteReservaAsync(long reservaId)
        {
            var sql = @"
                    DELETE FROM public.reservas
	                WHERE id = @Id";
            var result = await _baseAbierta.ExecuteAsync(sql, new { Id = reservaId });
            return result > 0;
        }

        public async Task<ReservaModel> GetReservaAsync(long reservaId)
        {
            var sql = @"SELECT * FROM public.reservas WHERE id = " + reservaId.ToString();
            return await _baseAbierta.QueryFirstOrDefaultAsync<ReservaModel>(sql, new ReservaModel { });
        }

        public async Task<IEnumerable<ReservaModel>> GetReservasAsync(string nombre)
        {
            string sql;
            if (nombre != "")
            {
                sql = @"SELECT * FROM public.reservas WHERE nombre= '" + nombre + "' ORDER BY id ASC";
            }
            else
            {
                sql = @"SELECT * FROM public.reservas ORDER BY id ASC";
            }
           
            return await _baseAbierta.QueryAsync<ReservaModel>(sql, new { });
        }
        public async Task<IEnumerable<UsuarioEmpastadoModel>> GetReservasTipoUsuarioAsync(string tipo)
        {
            string sql="";
            if (tipo == "manual")
            {
                sql = @"SELECT res.id as idreserva,emp.id as IdEmpastado,emp.facultad,emp.carrera,emp.tituloTesis,emp.trabajo,emp.autor,emp.tutor,emp.mes,emp.anio,emp.cantidad,us.id as IdUsuario,us.nombre,us.email,us.rol,res.tipousuario FROM public.reservas as res,public.empastados as emp,public.usuarios as us WHERE res.nombre='Empastado' and res.estado and res.iditem=emp.id and us.id=res.idusuario and res.tipousuario='manual';";
            }
            if(tipo=="gmail")
            {
                sql = @"SELECT res.id as idreserva,emp.id as IdEmpastado,emp.facultad,emp.carrera,emp.tituloTesis,emp.trabajo,emp.autor,emp.tutor,emp.mes,emp.anio,emp.cantidad,us.id as IdUsuario,us.name as nombre,us.email,us.rol,res.tipousuario FROM public.reservas as res,public.empastados as emp,public.usuariosgmail as us WHERE res.nombre='Empastado' and res.estado and res.iditem=emp.id and us.id=res.idusuario and res.tipousuario='gmail';";
            }

            return await _baseAbierta.QueryAsync<UsuarioEmpastadoModel>(sql, new { });
        }
        public async Task<IEnumerable<UsuarioEmpastadoModel>> GetReservasEmpastadosAsync()
        {
            var sql1 = @"SELECT res.id as idreserva,emp.id as IdEmpastado,emp.facultad,emp.carrera,emp.tituloTesis,emp.trabajo,emp.autor,emp.tutor,emp.mes,emp.anio,emp.cantidad,us.id as IdUsuario,us.nombre,us.email,us.rol,res.tipousuario FROM public.reservas as res,public.empastados as emp,public.usuarios as us WHERE res.nombre='Empastado' and res.estado and res.iditem=emp.id and us.id=res.idusuario and (res.tipousuario='Manual' or res.tipousuario='manual');";
            var usuarios = await _baseAbierta.QueryAsync<UsuarioEmpastadoModel>(sql1, new { });
            var sql2 = @"SELECT res.id as idreserva,emp.id as IdEmpastado,emp.facultad,emp.carrera,emp.tituloTesis,emp.trabajo,emp.autor,emp.tutor,emp.mes,emp.anio,emp.cantidad,us.id as IdUsuario,us.name as nombre,us.email,us.rol,res.tipousuario FROM public.reservas as res,public.empastados as emp,public.usuariosgmail as us WHERE res.nombre='Empastado' and res.estado and res.iditem=emp.id and us.id=res.idusuario and (res.tipousuario='Gmail' or res.tipousuario='gmail');";
            var usuariosGmail = await _baseAbierta.QueryAsync<UsuarioEmpastadoModel>(sql2, new { });
            List<UsuarioEmpastadoModel> list = new List<UsuarioEmpastadoModel>();
            foreach(var i in usuarios)
            {
                list.Add(i);
            }
            foreach (var i in usuariosGmail)
            {
                list.Add(i);
            }
            return list.OrderBy(i=>i.idreserva);
        }
        public async Task<ReservaModel> UpdateReservaAsync(long reservaId, ReservaModel actualizarReserva)
        {

            var sql = @"
                    UPDATE public.reservas
	                SET iditem=coalesce(@IdItem,iditem), idusuario=coalesce(@IdUsuario,idusuario),nombre=coalesce(@Nombre,nombre),tipousuario=coalesce(@TipoUsuario,tipousuario)
	                WHERE id = @Id";
            var result = await _baseAbierta.ExecuteAsync(sql, new { actualizarReserva.IdItem, actualizarReserva.IdUsuario, actualizarReserva.Nombre, actualizarReserva.TipoUsuario,Id= reservaId });
            var resultToShow = await GetReservaAsync(reservaId); 
            return resultToShow;
        }

       
    }
}
