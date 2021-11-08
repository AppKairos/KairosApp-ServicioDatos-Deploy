using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.BusinessLogicLayer.Services.Interfaces;
using ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.PresentationLayer.Controllers
{
    [Route("api/[controller]")]

    public class AfichesController: Controller
    {
        private IAficheService _aficheService;

        public AfichesController(IAficheService aficheService)
        {
            _aficheService = aficheService;
        }

        /// <summary>
        /* Permite hacer una cotizacion en el cual se require los campos AficheModel
           los importantes son:
            Color ej (Full Color,Duo Tono,Mono Color )
            
            gramaje del papel ej(Bond 63, Bond 75*,Bond 90)
            cantidad
            tamanio de la placa ej (GTO 52x33, ROLAND 52X72)
            Precio_Design
            Precio_Acabado
            Ganancia % es un porcentaje (0.1.........)
            
            con estos campos podemos calcular lo que seria ya 
            Cotizacion con Factura
            Cotizacion sin Factura
            Iva
            Total a pagar = Cotizacion con Factura

         */
        /// </summary>

        /// <returns></returns>
        [HttpPost("cotizar")]
        public async Task<ActionResult<CotizarAficheModel>> PostCotizacion([FromBody]AficheModel aficheModel) {
             
            try
            {
                var Cotizacion = await _aficheService.GetCotizacionAsync(aficheModel);
                Cotizacion.Neto = (Double)(Math.Round((decimal)Cotizacion.Neto, 2));
                Cotizacion.IVA = (Double)(Math.Round((decimal)Cotizacion.IVA, 2));
                Cotizacion.Con_Factura = (Double)(Math.Round((decimal)Cotizacion.Con_Factura, 2));
                Cotizacion.Total = (Double)(Math.Round((decimal)Cotizacion.Total, 2));

                return Ok(Cotizacion);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo inesperado Paso.");
            }
        }
        
    }
}
