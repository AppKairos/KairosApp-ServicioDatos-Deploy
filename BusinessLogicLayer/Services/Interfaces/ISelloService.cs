using ImprentaAPI.BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Services.Interfaces
{
    public interface ISelloService
    {
        public Task<IEnumerable<SelloModel>> GetSellossAsync(string orderBy = "Id");
        public Task<SelloModel> GetSelloAsync(long selloId);
        public Task<SelloModel> CreateSelloAsync(SelloModel nuevoSello);
        public Task<SelloModel> UpdateSelloAsync(long selloId, SelloModel actualizarSello);
        public Task<Boolean> DeleteSelloAsync(long selloId);

    }
}
