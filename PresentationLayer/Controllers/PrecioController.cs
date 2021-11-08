using ImprentaAPI.BusinessLogicLayer.Models;
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
    public class PrecioController:Controller
    {
        private IPrecioService _precioService;
        public PrecioController(IPrecioService precioService)
        {
            _precioService = precioService; 
        }
        /// <summary>
        /// Obtiene todos los precios de la imprenta kairos
        ///  /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrecioModel>>> GetPreciosAsync(string nombre="")
        {
            try
            {
                var precios = await _precioService.GetPreciosAsync(nombre);
                return Ok(precios);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo inesperado Paso.");
            }
        }
        /// <summary>
        /// Obtiene todos los precios por su nombre sin repetidos de la imprenta kairos
        ///  /// </summary>
        /// <returns></returns>
        [HttpGet("nombre")]
        public async Task<ActionResult<IEnumerable<string>>> GetPreciosNombreAsync()
        {
            try
            {
                var precios = await _precioService.GetPreciosNombreAsync();
                return Ok(precios);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo inesperado Paso.");
            }
        }
        /// <summary>
        /// Obtiene todos los nombres de la tabla precio dado un nombre de la imprenta kairos
        ///  /// </summary>
        /// <returns></returns>
        [HttpGet("tipo/{nombre}")]
        public async Task<ActionResult<IEnumerable<string>>> GetPreciosTipoxNombreAsync(string nombre)
        {
            try
            {
                var tipos = await _precioService.GetPreciosTipoxNombreAsync(nombre);
                return Ok(tipos);   
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

        [HttpGet("{precioId:long}")]
        public async Task<ActionResult<PrecioModel>> GetPrecioAsync(long precioId)
        {
            try
            {

                var precio = await _precioService.GetPrecioAsync(precioId);
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
        public async Task<ActionResult<PrecioModel>> CreatePrecioAsync([FromBody] PrecioModel nuevoPrecio)
        { 
            try
            {
                var precio = await _precioService.CreatePrecioAsync(nuevoPrecio);
                return Created($"api/reserva/{precio.id}", precio);
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

        [HttpPut("{precioId:long}")]
        public async Task<ActionResult<PrecioModel>> UpdatePrecioAsync(long precioId, [FromBody] PrecioModel actualizarPrecio)
        { 
            try
            {
                var precio = await _precioService.UpdatePrecioAsync(precioId, actualizarPrecio);
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

        [HttpDelete("{precioId:long}")]
        public async Task<ActionResult<bool>> DeletePrecioAsync(long precioId)
        { 
            try
            {
                var respuesta = await _precioService.DeletePrecioAsync(precioId);
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
