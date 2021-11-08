using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.BusinessLogicLayer.Services.Interfaces;
using ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Services
{

    public class AficheService : IAficheService
    {

        private readonly IAficheRepository _aficheRepository;

        public AficheService(IAficheRepository aficheRepository)
        {
            _aficheRepository = aficheRepository;
        }

        public async Task<CotizarAficheModel> GetCotizacionAsync(AficheModel aficheModel)
        {
            return await _aficheRepository.GetCotizacionAsync(aficheModel);
        }

    }
}
