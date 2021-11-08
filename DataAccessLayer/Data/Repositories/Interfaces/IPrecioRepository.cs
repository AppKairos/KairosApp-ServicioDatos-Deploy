using ImprentaAPI.BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces
{
    public interface IPrecioRepository
    {
        public Task<IEnumerable<PrecioModel>> GetPreciosAsync(string nombre);
        public Task<PrecioModel> GetPrecioAsync(long precioId);
        public Task<PrecioModel> CreatePrecioAsync(PrecioModel nuevaPrecio);
        public Task<PrecioModel> UpdatePrecioAsync(long precioId, PrecioModel actualizarPrecio);
        public Task<Boolean> DeletePrecioAsync(long precioId);
        public Task<IEnumerable<string>> GetPreciosNombreAsync();
        public Task<IEnumerable<string>> GetPreciosTipoxNombreAsync(string nombre);

    }
}
