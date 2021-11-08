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
    public class PrecioService : IPrecioService
    {
        private readonly IPrecioRepository _precioRepository; 
        public PrecioService(IPrecioRepository precioRepository)
        {
            _precioRepository = precioRepository;
        }
        public async Task<PrecioModel> CreatePrecioAsync(PrecioModel _nuevoPrecio)
        {
            var nuevoPrecio = await _precioRepository.CreatePrecioAsync(_nuevoPrecio);
            if (nuevoPrecio != null)
            {
                return nuevoPrecio; 
            }
            throw new Exception("No se pudo crear el Precio");
        }

        public async Task<bool> DeletePrecioAsync(long precioId)
        {
            await GetPrecioAsync(precioId);
            var PrecioBorrado = await _precioRepository.DeletePrecioAsync(precioId);
            if (PrecioBorrado)
            {
                return true;
            }
            throw new Exception("No se pudo eliminar el precio, revise su existencia");
        }

        public async Task<PrecioModel> GetPrecioAsync(long precioId)
        {
            var precio = await _precioRepository.GetPrecioAsync(precioId);
            if (precio == null)
            {
                throw new NotFoundPrecioException($"El precio con id {precioId} no existe");
            }
            return precio;
        }

        public async Task<IEnumerable<PrecioModel>> GetPreciosAsync(string nombre)
        {
            var precios = await _precioRepository.GetPreciosAsync(nombre);
            return precios; 
        }
        public async Task<IEnumerable<string>> GetPreciosTipoxNombreAsync(string nombre)
        {
            var tipos = await _precioRepository.GetPreciosTipoxNombreAsync(nombre);
            return tipos; 
        }
        public async Task<IEnumerable<string>> GetPreciosNombreAsync()
        {
            var nombres = await _precioRepository.GetPreciosNombreAsync();
            return nombres;
        }

        public async Task<PrecioModel> UpdatePrecioAsync(long precioId, PrecioModel actualizarPrecio)
        {
            await GetPrecioAsync(precioId);
            var res = await _precioRepository.UpdatePrecioAsync(precioId, actualizarPrecio);
            if (res == null)
            {
                throw new Exception("No se pudo actualizar el precio, revise su existencia");
            }
            return res;
        }
    }
}
