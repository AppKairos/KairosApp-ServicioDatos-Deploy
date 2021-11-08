using ImprentaAPI.BusinessLogicLayer.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces
{
    public interface IPrecioAcabadoRepository
    {
        public Task<IEnumerable<PrecioAcabadoModel>> GetPreciosAcabadoAsync(string nombre);
        public Task<PrecioAcabadoModel> GetPrecioAcabadoAsync(string nombre);
        public Task<PrecioAcabadoModel> CreatePrecioAcabadoAsync(PrecioAcabadoModel nuevaPrecio);
        public Task<PrecioAcabadoModel> UpdatePrecioAcabadoAsync(string nombre, PrecioAcabadoModel actualizarPrecio);
        public Task<Boolean> DeletePrecioAcabadoAsync(string nombre);

    }
}
