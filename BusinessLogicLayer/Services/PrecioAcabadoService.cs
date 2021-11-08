using ImprentaAPI.BusinessLogicLayer.Models.Security;
using ImprentaAPI.BusinessLogicLayer.Services.Interfaces;
using ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces;
using ImprentaAPI.PresentationLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Services
{
    public class PrecioAcabadoService : IPrecioAcabadoService
    {
        private readonly IPrecioAcabadoRepository _precioAcabadoRepository;
        public PrecioAcabadoService(IPrecioAcabadoRepository precioAcabadoRepository)
        {
            _precioAcabadoRepository = precioAcabadoRepository;
        }
        public async Task<PrecioAcabadoModel> CreatePrecioAcabadoAsync(PrecioAcabadoModel nuevoPrecio)
        {
            var nuevoPrecioI = await _precioAcabadoRepository.CreatePrecioAcabadoAsync(nuevoPrecio);
            if (nuevoPrecioI != null)
            {
                return nuevoPrecioI;
            }
            throw new Exception("No se pudo crear el Precio");
        }

        public async Task<bool> DeletePrecioAcabadoAsync(string nombre)
        {
            var PrecioBorrado = await _precioAcabadoRepository.DeletePrecioAcabadoAsync(nombre);
            if (PrecioBorrado)
            {
                return true;
            }
            throw new Exception("No se pudo eliminar el precio, revise su existencia");
        }

        public async Task<PrecioAcabadoModel> GetPrecioAcabadoAsync(string nombre)
        {
            var precio = await _precioAcabadoRepository.GetPrecioAcabadoAsync(nombre);
            if (precio == null)
            {
                throw new NotFoundPrecioException($"El precio del producto {nombre} no existe");
            }
            return precio;
        }

        public async Task<IEnumerable<PrecioAcabadoModel>> GetPreciosAcabadoAsync(string nombre)
        {
            var precios = await _precioAcabadoRepository.GetPreciosAcabadoAsync(nombre);
            if (precios!=null)
            {
                return precios;
            }
            else
            {
                return null;
            }
        }

        public async Task<PrecioAcabadoModel> UpdatePrecioAcabadoAsync(string nombre, PrecioAcabadoModel actualizarPrecio)
        {
            var res = await _precioAcabadoRepository.UpdatePrecioAcabadoAsync(nombre, actualizarPrecio);
            if (res == null)
            {
                throw new Exception("No se pudo actualizar el precio, revise su existencia");
            }
            return res;
        }
    }
}
