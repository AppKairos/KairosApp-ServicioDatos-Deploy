using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.BusinessLogicLayer.Services.Interfaces;
using ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces;
using ImprentaAPI.PresentationLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Services
{
    public class ReservaService : IReservaService
    {

        private readonly IReservaRepository _reservaRepository; 
        public ReservaService(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public async Task<ReservaModel> CreateReservaAsync(ReservaModel _nuevaReserva)
        {
            var nuevaReserva = await _reservaRepository.CreateReservaAsync(_nuevaReserva);
            if (nuevaReserva != null) 
            {
                return nuevaReserva;
            }
            throw new Exception("No se pudo crear la Reserva");
        }

        public async Task<bool> DeleteReservaAsync(long reservaId)
        {
            await GetReservaAsync(reservaId);
            var ReservaBorrada = await _reservaRepository.DeleteReservaAsync(reservaId);
            if (ReservaBorrada)
            {
                return true;
            }
            throw new Exception("No se pudo eliminar la reserva, revise su existencia");
        }

        public async Task<ReservaModel> GetReservaAsync(long reservaId)
        {
            var reserva = await _reservaRepository.GetReservaAsync(reservaId);
            if (reserva == null)
            {
                throw new NotFoundReservaException($"La reserva con id {reservaId} no existe");
            } 
            return reserva;
        }

        public async Task<IEnumerable<ReservaModel>> GetReservasAsync(string nombre)
        {
            var reservas = await _reservaRepository.GetReservasAsync(nombre); 
            return reservas;
        }

        public async Task<IEnumerable<UsuarioEmpastadoModel>> GetReservasEmpastadosAsync()
        {
            var reservas = await _reservaRepository.GetReservasEmpastadosAsync();
            return reservas;
        }

        public async Task<IEnumerable<UsuarioEmpastadoModel>> GetReservasTipoUsuarioAsync(string tipo)
        {
            var reservas = await _reservaRepository.GetReservasTipoUsuarioAsync(tipo);
            return reservas;
        }

        public async Task<ReservaModel> UpdateReservaAsync(long reservaId, ReservaModel actualizarReserva)
        {
            await GetReservaAsync(reservaId);
            var res = await _reservaRepository.UpdateReservaAsync(reservaId, actualizarReserva);
            if (res == null)
            {
                throw new Exception("No se pudo actualizar la reserva, revise su existencia");
            }
            return res;
        }
    }
}
