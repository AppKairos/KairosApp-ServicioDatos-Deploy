using ImprentaAPI.BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Services
{
    public interface ITokenService
    {
        String CreateToken(UsuarioModel usuario);
    }
}
