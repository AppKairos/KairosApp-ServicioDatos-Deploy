using ImprentaAPI.BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Services.Interfaces
{
    public interface IAficheService
    {
        public Task<CotizarAficheModel> GetCotizacionAsync(AficheModel aficheModel);
         
    }
}
