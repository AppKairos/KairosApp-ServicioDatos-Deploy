using ImprentaAPI.BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces
{
    public interface IAficheRepository
    {
        public Task<CotizarAficheModel> GetCotizacionAsync(AficheModel aficheModel);
        public Task<PrecioModel> GetPrecioGramajeBDAsync(string nombre,string Gramaje);

    }
}
