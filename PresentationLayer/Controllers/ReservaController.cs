using ImprentaAPI.Exceptions;
using ImprentaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces;
using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.PresentationLayer.Exceptions;
using ImprentaAPI.BusinessLogicLayer.Services.Interfaces;

namespace ImprentaAPI.PresentationLayer.Controllers
{
    [Route("api/[controller]")]

    public class ReservaController:Controller
    {
        private IReservaService _reservaService;
        public ReservaController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }
        /// <summary>
        /// Obtiene todos las reservas de la imprenta kairos
        ///  /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaModel>>> GetReservasAsync(string nombre="")
        {
            try
            {
                var reservas = await _reservaService.GetReservasAsync(nombre);
                return Ok(reservas); 
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo inesperado Paso.");
            }
        }
        /// <summary>
        /// Obtiene todos las reservas con sus campos de empastados imprenta kairos
        ///  /// </summary>
        /// <returns></returns>
        [HttpGet("empastados")]
        public async Task<ActionResult<IEnumerable<ReservaModel>>> GetReservasEmpastadosAsync()
        {
            try
            {
                var reservas = await _reservaService.GetReservasEmpastadosAsync();
                return Ok(reservas);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo inesperado Paso.");
            }
        }
        /// <summary>
        /// Obtiene todos las reservas dependiendo al tipo si es manual o gmail imprenta kairos
        ///  /// </summary>
        /// <returns></returns>
        [HttpGet("tipoUsuario/{nombre}")]
        public async Task<ActionResult<IEnumerable<ReservaModel>>> GetReservasTipoUsuarioAsync(string nombre = "")
        { 
            try
            {
                var reservas = await _reservaService.GetReservasTipoUsuarioAsync(nombre);
                return Ok(reservas); 
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo inesperado Paso.");
            }
        }
        /// <summary>
        /// Obtiene una reserva especifica segun el id que es requisito enviarlo por meedio de la peticion 
        /// </summary>
        /// <param name="reservaId"></param>
        /// <returns></returns>

        [HttpGet("{reservaId:long}")]
        public async Task<ActionResult<ReservaModel>> GetReservaAsync(long reservaId) 
        {
            try 
            {

                var reserva = await _reservaService.GetReservaAsync(reservaId);
                return Ok(reserva);
            }  
            catch (NotFoundReservaException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite crear una nueva reserva 
        /// </summary>
        /// <param name="nuevaReserva"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<ReservaModel>> CreateReservaAsync([FromBody] ReservaModel nuevaReserva)
        {
            try
            {
                var reserva = await _reservaService.CreateReservaAsync(nuevaReserva);
                return Created($"api/reserva/{reserva.Id}", reserva);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite actualizar una reserva ya existente mediante su id
        /// </summary>
        /// <param name="reservaId"></param>
        /// <param name="actualizarReserva"></param>
        /// <returns></returns>

        [HttpPut("{reservaId:long}")]
        public async Task<ActionResult<ReservaModel>> UpdateReservaAsync(long reservaId, [FromBody] ReservaModel actualizarReserva)
        {
            try
            { 
                var reserva = await _reservaService.UpdateReservaAsync(reservaId, actualizarReserva);
                return Ok(reserva);
            }
            catch (NotFoundReservaException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite eliminar una reserva ya existente mediante su id
        /// </summary>
        /// <param name="reservaId"></param>
        /// <returns></returns>

        [HttpDelete("{reservaId:long}")] 
        public async Task<ActionResult<bool>> DeleteReservaAsync(long reservaId)
        { 
            try
            {
                var respuesta = await _reservaService.DeleteReservaAsync(reservaId);
                return Ok(respuesta);
            }
            catch (NotFoundReservaException ex)
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
