using ImprentaAPI.BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Services.Interfaces
{
    public interface IReservaService
    {
        public Task<IEnumerable<ReservaModel>> GetReservasAsync(string nombre);
        public Task<ReservaModel> GetReservaAsync(long reservaId);
        public Task<ReservaModel> CreateReservaAsync(ReservaModel nuevaReserva);
        public Task<ReservaModel> UpdateReservaAsync(long reservaId, ReservaModel actualizarReserva);
        public Task<Boolean> DeleteReservaAsync(long reservaId);
        public Task<IEnumerable<UsuarioEmpastadoModel>> GetReservasTipoUsuarioAsync(string tipo);
        public Task<IEnumerable<UsuarioEmpastadoModel>> GetReservasEmpastadosAsync();
 

    }
}
