using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.BusinessLogicLayer.Services.Interfaces;
using ImprentaAPI.Exceptions;
using ImprentaAPI.Models;
using ImprentaAPI.PresentationLayer.Exceptions;
using ImprentaAPI.Services;
using ImprentaAPI.Services.Interfaces;
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

    public class EmpastadoController : Controller
    {
        private IEmpastadoService _empastadoService;
        public EmpastadoController(IEmpastadoService empastadoService)
        {
            _empastadoService = empastadoService;
        }

        /// <summary>
        /// Obtiene todos los empastados que ofrece la imprenta kairos
        /// <param name="orderBy"></param>
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpastadoModel>>> GetEmpastadosAsync(string orderBy = "Id")
        {
            try
            {
                var empastados = await _empastadoService.GetEmpastadosAsync(orderBy);
                return Ok(empastados);
            }
            catch (InvalidOperationEmpastadoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo inesperado Paso.");
            }
        }

        /// <summary>
        /// Obtiene un empastado especifico segun el id que es requisito enviarlo por meedio de la peticion 
        /// </summary>
        /// <param name="empastadoId"></param>
        /// <returns></returns>

        [HttpGet("{empastadoId:long}")]
        public async Task<ActionResult<EmpastadoModel>> GetEmpastadoAsync(long empastadoId)
        {
            try
            {

                var empastado = await _empastadoService.GetEmpastadoAsync(empastadoId);
                return Ok(empastado);
            }
            catch (NotFoundEmpastadoException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite crear un nuevo empastado 
        /// </summary>
        /// <param name="nuevoEmpastado"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<EmpastadoModel>> CreateEmpastadoAsync([FromBody] EmpastadoModel nuevoEmpastado)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var empastado = await _empastadoService.CreateEmpastadoAsync(nuevoEmpastado);
                return Created($"api/empastado/{nuevoEmpastado.Id}", nuevoEmpastado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite actualizar un empastado ya existente mediante su id
        /// </summary>
        /// <param name="empastadoId"></param>
        /// <param name="actualizarEmpastado"></param>
        /// <returns></returns>

        [HttpPut("{empastadoId:long}")]
        public async Task<ActionResult<EmpastadoModel>> UpdateEmpastadoAsync(long empastadoId, [FromBody] EmpastadoModel actualizarEmpastado)
        {
            try
            {
                var empastado = await _empastadoService.UpdateEmpastadoAsync(empastadoId, actualizarEmpastado);
                return Ok(empastado);
            }
            catch (NotFoundEmpastadoException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite eliminar un empastado ya existente mediante su id
        /// </summary>
        /// <param name="empastadoId"></param>
        /// <returns></returns>

        [HttpDelete("{empastadoId:long}")]
        public async Task<ActionResult<bool>> DeleteEmpastadoAsync(long empastadoId)
        {
            try
            {
                var respuesta = await _empastadoService.DeleteEmpastadoAsync(empastadoId);
                return Ok(respuesta);
            }
            catch (NotFoundEmpastadoException ex)
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
