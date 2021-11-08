using ImprentaAPI.BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces
{
    public interface IReservaRepository
    {
        public Task<IEnumerable<ReservaModel>> GetReservasAsync(string tipo);
        public Task<ReservaModel> GetReservaAsync(long reservaId); 
        public Task<ReservaModel> CreateReservaAsync(ReservaModel nuevaReserva); 
        public Task<ReservaModel> UpdateReservaAsync(long reservaId, ReservaModel actualizarReserva);
        public Task<Boolean> DeleteReservaAsync(long reservaId);
        public Task<IEnumerable<UsuarioEmpastadoModel>> GetReservasTipoUsuarioAsync(string tipo);
        public Task<IEnumerable<UsuarioEmpastadoModel>> GetReservasEmpastadosAsync();

    }
}
