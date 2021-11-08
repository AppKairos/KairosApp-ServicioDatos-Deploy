using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.BusinessLogicLayer.Models.Security;
using ImprentaAPI.BusinessLogicLayer.Services.Interfaces;
using ImprentaAPI.PresentationLayer.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    public class PrecioAcabadoController : Controller
    {
        private IPrecioAcabadoService _precioAcabadoService;
        public PrecioAcabadoController(IPrecioAcabadoService precioAcabadoService)
        {
            _precioAcabadoService = precioAcabadoService;
        }
        /// <summary>
        /// Obtiene todos los precios de la imprenta kairos
        ///  /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrecioAcabadoModel>>> GetPreciosAsync(string nombre = "")
        {
            try
            {
                var precios = await _precioAcabadoService.GetPreciosAcabadoAsync(nombre);
                return Ok(precios);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo inesperado Paso.");
            }
        }
        
        /// <summary>
        /// Obtiene un precio especifico segun el id que es requisito enviarlo por meedio de la peticion 
        /// </summary>
        /// <param name="precioId"></param>
        /// <returns></returns>

        [HttpGet("{nombre}")]
        public async Task<ActionResult<PrecioAcabadoModel>> GetPrecioAcabadoAsync(string nombre)
        {
            try
            {

                var precio = await _precioAcabadoService.GetPrecioAcabadoAsync(nombre);
                return Ok(precio);
            }
            catch (NotFoundPrecioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite crear un nuevo precio 
        /// </summary>
        /// <param name="nuevoPrecio"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<PrecioAcabadoModel>> CreatePrecioAcabadoAsync([FromBody] PrecioAcabadoModel nuevoPrecio)
        {
            try
            {
                var precio = await _precioAcabadoService.CreatePrecioAcabadoAsync(nuevoPrecio);
                return Created($"api/reserva/{precio.Id}", precio);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite actualizar un precio ya existente mediante su id
        /// </summary>
        /// <param name="precioId"></param>
        /// <param name="actualizarPrecio"></param>
        /// <returns></returns>

        [HttpPut("{nombre}")]
        public async Task<ActionResult<PrecioAcabadoModel>> UpdatePrecioAsync(string nombre, [FromBody] PrecioAcabadoModel actualizarPrecio)
        {
            try
            {
                var precio = await _precioAcabadoService.UpdatePrecioAcabadoAsync(nombre, actualizarPrecio);
                return Ok(precio);
            }
            catch (NotFoundPrecioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite eliminar un precio ya existente mediante su id
        /// </summary>
        /// <param name="precioId"></param>
        /// <returns></returns>

        [HttpDelete("{nombre}")]
        public async Task<ActionResult<bool>> DeletePrecioAsync(string nombre)
        {
            try
            {
                var respuesta = await _precioAcabadoService.DeletePrecioAcabadoAsync(nombre);
                return Ok(respuesta);
            }
            catch (NotFoundPrecioException ex)
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

