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
    public class EmpastadoService : IEmpastadoService
    {
        private readonly IEmpastadoRepository _empastadoRepository;
        public EmpastadoService(IEmpastadoRepository empastadoRepository)
        {
            _empastadoRepository = empastadoRepository;
        }

        public async Task<EmpastadoModel> CreateEmpastadoAsync(EmpastadoModel newEmpastado)
        {
            var nuevoEmpastado = await _empastadoRepository.CreateEmpastadoAsync(newEmpastado);
            if (nuevoEmpastado != null)
            {
                return nuevoEmpastado;
            }
            throw new Exception("No se pudo crear el empastado");
        }

        public async Task<bool> DeleteEmpastadoAsync(long empastadoId)
        {
            var empastadoBorrado = await _empastadoRepository.DeleteEmpastadoAsync(empastadoId);
            if (empastadoBorrado)
            {
                return true;
            }
            throw new Exception("No se pudo eliminar el empastado, revise su existencia");

        }

        public async Task<EmpastadoModel> GetEmpastadoAsync(long empastadoId)
        {
            var empastado = await _empastadoRepository.GetEmpastadoAsync(empastadoId);
            if (empastado == null)
            {
                throw new NotFoundEmpastadoException($"El empastado con id {empastadoId} no existe");
            }
            return empastado;
        }

        public async Task<IEnumerable<EmpastadoModel>> GetEmpastadosAsync(string orderBy = "Id")
        {
            var empastadosLista = await _empastadoRepository.GetEmpastadosAsync(orderBy.ToLower());
            return empastadosLista;
        }

        public async Task<EmpastadoModel> UpdateEmpastadoAsync(long empastadoId, EmpastadoModel actualizarEmpastado)
        {
            var resultado = await _empastadoRepository.UpdateEmpastadoAsync(empastadoId, actualizarEmpastado);
            if (resultado == null)
            {
                throw new Exception("No se pudo actualizar el empastado, revise su existencia");
            }
            return resultado;
        }
    }
}
