using ImprentaAPI.BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces
{
    public interface IEmpastadoRepository
    {
        public Task<IEnumerable<EmpastadoModel>> GetEmpastadosAsync(string orderBy = "Id");
        public Task<EmpastadoModel> GetEmpastadoAsync(long productoId);
        public Task<EmpastadoModel> CreateEmpastadoAsync(EmpastadoModel nuevoEmpastado);
        public Task<EmpastadoModel> UpdateEmpastadoAsync(long productoId, EmpastadoModel actualizarEmpastado);
        public Task<Boolean> DeleteEmpastadoAsync(long EmpastadoId);

    }
}
