using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.BusinessLogicLayer.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.DataAccessLayer.Data.Repositories
{
    public interface IAuthRepository
    {
        Task<UsuarioToShowModel> RegistrarAsync(UsuarioModel usuario, String password);
        Task<UsuarioModel> LoginAsync(string email, String password);
        Task<UsuarioToShowModel> LoginToShowAsync(string email);
        Task<Boolean> ExisteUsuario(string email);
        Task<Boolean> ExisteUsuarioGmail(String auth);
        Task<UsuarioGmailModel> RegistrarGmailAsync(UsuarioGmailModel usuario);
        Task<UsuarioToShowGmailModel> LoginGmailAsync(UsuarioGmailModel usuario);
        Task<Boolean> DeleteClienteAsync(long clienteId);
        Task<Boolean> DeleteClienteGmailAsync(long clienteId);
        Task<IEnumerable<UsuarioToShowModel>> GetUsuariosAsync(long usuarioId);
        Task<IEnumerable<UsuarioToShowGmailModel>> GetUsuariosGmailAsync(long usuarioId);
        Task<UsuarioToShowModel> GetUsuarioAsync( long usuario);
        Task<UsuarioToShowGmailModel> GetUsuarioGmailAsync(long usuario);
        Task<UsuarioToShowModel> UpdateUsuarioAsync(long usuarioId,UsuarioToShowModel actualizarUsuario);
        Task<UsuarioToShowGmailModel> UpdateUsuarioGmailAsync(long usuarioId, UsuarioToShowGmailModel actualizarUsuario);
    }
}
