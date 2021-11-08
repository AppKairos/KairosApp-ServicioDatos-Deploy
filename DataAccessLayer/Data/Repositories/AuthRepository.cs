using Dapper;
using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.BusinessLogicLayer.Models.Security;
using ImprentaAPI.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.DataAccessLayer.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private PostgreSQLConfiguration _connection;
        private NpgsqlConnection _baseAbierta;

        private bool VerifyPasswordHash(String password,byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i=0;i<computedHash.Length;i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
        protected NpgsqlConnection AbreConexion()
        {
            return _connection.AbreConexion();
        }
        public AuthRepository(PostgreSQLConfiguration connectionString)
        {
            _connection = connectionString;
            _baseAbierta = AbreConexion();
        }
        public async Task<bool> ExisteUsuario(string email)
        {
            bool respuesta = false;
 
            var sql = @"SELECT * FROM public.usuarios WHERE email='" + email.ToString()+"'";
            var ejecucion = await _baseAbierta.QueryFirstOrDefaultAsync<UsuarioModel>(sql, new UsuarioModel { });
            if (ejecucion != null)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<UsuarioModel> LoginAsync(string email, string password)
        {
 
            var sql = @"SELECT * FROM public.usuarios WHERE email='" + email.ToString()+"'";
            var usuario = await _baseAbierta.QueryFirstOrDefaultAsync<UsuarioModel>(sql, new UsuarioModel { });
            if(usuario ==null)
            {
                return null;
            }
            if(!VerifyPasswordHash(password,usuario.PasswordHash,usuario.PasswordSalt))
            {
                return null;
            }
            return usuario;
        }

        public async Task<UsuarioToShowModel> RegistrarAsync(UsuarioModel usuario, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;
            var existSql = "select email from public.usuarios where email='" + usuario.Email.ToString() + "'";
            var result = await _baseAbierta.QueryFirstOrDefaultAsync<string>(existSql, new { });
            if (result==null)
            {
                var sql = @"
                    INSERT INTO public.usuarios(nombre, email, rol, activo, passwordhash, passwordSalt, telefono,tipoUsuario)
                    VALUES (@Nombre, @Email, @Rol, @Activo, @PasswordHash, @PasswordSalt, @Telefono,@TipoUsuario)";
                var resultado = await _baseAbierta.ExecuteAsync(sql, new { usuario.Id, usuario.Nombre, usuario.Email, usuario.Rol, usuario.Activo, usuario.PasswordHash, usuario.PasswordSalt, usuario.Telefono, usuario.TipoUsuario });
                var sql2 = @"SELECT * FROM public.usuarios ORDER BY id DESC LIMIT 1";
                var resultToShow = await _baseAbierta.QueryFirstOrDefaultAsync<UsuarioToShowModel>(sql2, new UsuarioToShowModel { });
                return resultToShow;
            }
            return null;
        }

        public async Task<UsuarioToShowModel> LoginToShowAsync(string email)
        {
 
            var sql = @"SELECT * FROM public.usuarios WHERE email='" + email.ToString()+"'";
            var usuario = await _baseAbierta.QueryFirstOrDefaultAsync<UsuarioToShowModel>(sql, new UsuarioToShowModel { });
            if (usuario == null)
            {
                return null;
            }
            return usuario;
        }

        public async Task<bool> DeleteClienteAsync(long clienteId)
        {
            var sql = @"
                    UPDATE public.usuarios
                    SET activo=false
	                WHERE id = @Id";
            var result = await _baseAbierta.ExecuteAsync(sql, new { Id = clienteId });
            return result > 0;
        }

        public async Task<bool> ExisteUsuarioGmail(string email)
        {
            bool respuesta = false;

            var sql = @"SELECT * FROM public.usuariosgmail WHERE email= '" + email.ToString()+ " ' ";
            var ejecucion = await _baseAbierta.QueryFirstOrDefaultAsync<UsuarioGmailModel>(sql, new  { });
            if (ejecucion != null)
            {
                respuesta = true;
            }
            return respuesta;
        }


        public async Task<UsuarioToShowGmailModel> LoginGmailAsync(UsuarioGmailModel usuario)
        {
            if (usuario.AuthToken!=null && usuario.IdToken!=null)
            {
                var sql = @"SELECT * FROM public.usuariosgmail WHERE email='" + usuario.Email.ToString() + "'";
                var usuarioI = await _baseAbierta.QueryFirstOrDefaultAsync<UsuarioToShowGmailModel>(sql, new { });
                if (usuarioI == null)
                {
                    sql = @"SELECT * FROM public.usuarios WHERE email='" + usuario.Email.ToString() + "'";
                    usuarioI = await _baseAbierta.QueryFirstOrDefaultAsync<UsuarioToShowGmailModel>(sql, new { });
                    if (usuarioI == null)
                    {
                        return usuarioI;
                    }
                    return usuarioI;
                }
                usuarioI.AuthToken = usuario.AuthToken;
                usuarioI.IdToken = usuario.IdToken;
                return usuarioI;
            }
            return null; 
        }

        public async Task<UsuarioGmailModel> RegistrarGmailAsync(UsuarioGmailModel usuario)
        {
            if (usuario.IdToken!=null && usuario.AuthToken!=null)
            {
                var existSql = "select email from public.usuariosgmail where email='" + usuario.Email.ToString() + "'";
                var result = await _baseAbierta.QueryFirstOrDefaultAsync<string>(existSql, new { });
                if (result == null)
                {
                    var sql = @"
                    INSERT INTO public.usuariosgmail(name, firstname, lastname, email, tipousuario, rol, activo)
                    VALUES (@Name, @FirstName, @LastName, @Email, @TipoUsuario, @Rol, @Activo)";
                    var resultado = await _baseAbierta.ExecuteAsync(sql, new { usuario.Id, usuario.Name, usuario.FirstName, usuario.LastName, usuario.Email, usuario.TipoUsuario, usuario.Rol, usuario.Activo });
                    var sql2 = @"SELECT * FROM public.usuariosgmail ORDER BY id DESC LIMIT 1";
                    var resultToShow = await _baseAbierta.QueryFirstOrDefaultAsync<UsuarioGmailModel>(sql2, new UsuarioGmailModel { });
                    return resultToShow;
                }
            }
            return null;
        }

        public async Task<bool> DeleteClienteGmailAsync(long clienteId)
        {
            var sql = @"
                    UPDATE public.usuariosgmail
                    SET activo=false
	                WHERE id = @Id";
            var result = await _baseAbierta.ExecuteAsync(sql, new { Id = clienteId });
            return result > 0;
        }

        public async Task<IEnumerable<UsuarioToShowModel>> GetUsuariosAsync(long usuarioId)
        {
            var consultAdmin = @"SELECT rol FROM public.usuarios WHERE id=" + usuarioId;
            var admin = await _baseAbierta.QueryFirstOrDefaultAsync<string>(consultAdmin);
            if (admin == "admin")
            {
                var sql = @"SELECT * FROM public.usuarios WHERE activo=true ORDER BY id ASC";
                return await _baseAbierta.QueryAsync<UsuarioToShowModel>(sql, new { });
            }
            return null;
        }

        public async Task<IEnumerable<UsuarioToShowGmailModel>> GetUsuariosGmailAsync(long usuarioId)
        {
            var consultAdmin = @"SELECT rol FROM public.usuarios WHERE id=" + usuarioId;
            var admin = await _baseAbierta.QueryFirstOrDefaultAsync<string>(consultAdmin);
            if (admin == "admin")
            {
                var sql = @"SELECT * FROM public.usuariosgmail WHERE activo=true ORDER BY id ASC";
                return await _baseAbierta.QueryAsync<UsuarioToShowGmailModel>(sql, new { });
            }
            return null;
        }

        public async Task<UsuarioToShowModel> GetUsuarioAsync(long usuario)
        {
            var sql = @"SELECT * FROM public.usuarios WHERE activo=true and id=" + usuario + "ORDER BY id ASC";
            return await _baseAbierta.QueryFirstOrDefaultAsync<UsuarioToShowModel>(sql, new { });
        }

        public async Task<UsuarioToShowGmailModel> GetUsuarioGmailAsync( long usuario)
        {
            var sql = @"SELECT * FROM public.usuariosgmail WHERE activo=true and id=" + usuario + "ORDER BY id ASC";
            return await _baseAbierta.QueryFirstOrDefaultAsync<UsuarioToShowGmailModel>(sql, new { });
        }

        public async Task<UsuarioToShowModel> UpdateUsuarioAsync(long usuarioId,UsuarioToShowModel actualizarUsuario)
        {
            var sql =@"UPDATE public.usuarios SET nombre=coalesce(@Nombre,nombre),email=coalesce(@Email,email),telefono=coalesce(@Telefono,telefono) WHERE id=@Id";
            var result = await _baseAbierta.ExecuteAsync(sql, new
            {
                actualizarUsuario.Nombre,
                actualizarUsuario.Email,
                actualizarUsuario.Telefono,
                Id = usuarioId
            });
            var resultToShow = await GetUsuarioAsync(usuarioId);
            return resultToShow;
        }

        public async Task<UsuarioToShowGmailModel> UpdateUsuarioGmailAsync(long usuarioId, UsuarioToShowGmailModel actualizarUsuario)
        {
            var sql = @"UPDATE public.usuariosgmail SET name=coalesce(@Name,name),firstname=coalesce(@FirstName,firstname),lastname=coalesce(@LastName,lastname) WHERE id=@Id";
            var result = await _baseAbierta.ExecuteAsync(sql, new
            {
                actualizarUsuario.Name,
                actualizarUsuario.FirstName,
                actualizarUsuario.LastName,
                Id = usuarioId
            });
            var resultToShow = await GetUsuarioGmailAsync(usuarioId);
            return resultToShow;
        }
    }
}
