using ImprentaAPI.BusinessLogicLayer.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Services.Interfaces
{
    public interface IPrecioAcabadoService
    {
        public Task<IEnumerable<PrecioAcabadoModel>> GetPreciosAcabadoAsync(string nombre);
        public Task<PrecioAcabadoModel> GetPrecioAcabadoAsync(string nombre);
        public Task<PrecioAcabadoModel> CreatePrecioAcabadoAsync(PrecioAcabadoModel nuevoPrecio);
        public Task<PrecioAcabadoModel> UpdatePrecioAcabadoAsync(string nombre, PrecioAcabadoModel actualizarPrecio);
        public Task<Boolean> DeletePrecioAcabadoAsync(string nombre);
    }
}
