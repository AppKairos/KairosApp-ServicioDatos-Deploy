using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.BusinessLogicLayer.Services;
using ImprentaAPI.BusinessLogicLayer.Services.Interfaces;
using ImprentaAPI.Exceptions;
using ImprentaAPI.Models;
using ImprentaAPI.PresentationLayer.Exceptions;
using ImprentaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ImprentaAPI.PresentationLayer.Controllers
{
    [Route("api/[controller]")]

    public class SelloController : Controller
    {
        private ISelloService _selloService;
        public SelloController(ISelloService selloService)
        {
            _selloService = selloService;
        }

        /// <summary>
        /// Obtiene todos los sellos que ofrece la imprenta kairos
        /// <param name="orderBy"></param>
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SelloModel>>> GetSellosAsync(string orderBy = "Id")
        {
            try
            {
                var sellos = await _selloService.GetSellossAsync(orderBy);
                return Ok(sellos);
            }
            catch (InvalidOperationSelloException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo inesperado Paso.");
            }
        }

        /// <summary>
        /// Obtiene un sello especifico segun el id que es requisito enviarlo por meedio de la peticion 
        /// </summary>
        /// <param name="selloId"></param>
        /// <returns></returns>

        [HttpGet("{selloId:long}")]
        public async Task<ActionResult<SelloModel>> GetSelloAsync(long selloId)
        {
            try
            {

                var sello = await _selloService.GetSelloAsync(selloId);
                return Ok(sello);
            }
            catch (NotFoundSelloException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite crear un nuevo sello 
        /// </summary>
        /// <param name="nuevoSello"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<SelloModel>> CreateSelloAsync([FromBody] SelloModel nuevoSello)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var sello = await _selloService.CreateSelloAsync(nuevoSello);
                return Created($"api/sello/{nuevoSello.Id}", nuevoSello);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite actualizar un sello ya existente mediante su id
        /// </summary>
        /// <param name="selloId"></param>
        /// <param name="actualizarSello"></param>
        /// <returns></returns>

        [HttpPut("{selloId:long}")]
        public async Task<ActionResult<SelloModel>> UpdateSelloAsync(long selloId, [FromBody] SelloModel actualizarSello)
        {
            try
            {
                var sello = await _selloService.UpdateSelloAsync(selloId, actualizarSello);
                return Ok(sello);
            }
            catch (NotFoundSelloException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite eliminar un sello ya existente mediante su id
        /// </summary>
        /// <param name="selloId"></param>
        /// <returns></returns>

        [HttpDelete("{selloId:long}")]
        public async Task<ActionResult<bool>> DeleteSelloAsync(long selloId)
        {
            try
            {
                var respuesta = await _selloService.DeleteSelloAsync(selloId);
                return Ok(respuesta);
            }
            catch (NotFoundSelloException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }






    }
}
