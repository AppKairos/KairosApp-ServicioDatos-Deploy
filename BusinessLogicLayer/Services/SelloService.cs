using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.BusinessLogicLayer.Services.Interfaces;
using ImprentaAPI.DataAccessLayer.Data.Repositories;
using ImprentaAPI.Exceptions;
using ImprentaAPI.PresentationLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Services
{
    public class SelloService:ISelloService
    {
        private readonly ISelloRepository _selloRepository;
        public SelloService(ISelloRepository selloRepository)
        {
            _selloRepository = selloRepository;
        }

        public async Task<SelloModel> CreateSelloAsync(SelloModel newSello)
        {
            var nuevoSello = await _selloRepository.CreateSelloAsync(newSello);
            if (nuevoSello != null)
            {
                return nuevoSello;
            }
            throw new Exception("No se pudo crear el sello");
        }

        public async Task<bool> DeleteSelloAsync(long selloId)
        {
            var selloBorrado = await _selloRepository.DeleteSelloAsync(selloId);
            if (selloBorrado)
            {
                return true;
            }
            throw new Exception("No se pudo eliminar el sello, revise su existencia");

        }

        public async Task<SelloModel> GetSelloAsync(long selloId)
        {
            var sello = await _selloRepository.GetSelloAsync(selloId);
            if (sello == null)
            {
                throw new NotFoundSelloException($"El sello con id {selloId} no existe");
            }
            return sello;
        }

        public async Task<IEnumerable<SelloModel>> GetSellossAsync(string orderBy = "Id")
        {
            var sellosLista = await _selloRepository.GetSellosAsync(orderBy.ToLower());
            return sellosLista;
        }

        public async Task<SelloModel> UpdateSelloAsync(long selloId, SelloModel actualizarSello)
        {
            var resultado = await _selloRepository.UpdateSelloAsync(selloId, actualizarSello);
            if (resultado == null)
            {
                throw new Exception("No se pudo actualizar el sello, revise su existencia");
            }
            return resultado;
        }
    }
}

