using ImprentaAPI.Exceptions;
using ImprentaAPI.Models;
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

namespace ImprentaAPI.Controllers
{
    [Route("api/[controller]")]

    public class ProductoController : Controller
    {
        private IProductoService _productoService;
        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        /// <summary>
        /// Obtiene todos los productos que ofrece la imprenta kairos
        /// <param name="orderBy"></param>
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoModel>>> GetProductosAsync(string orderBy = "Id")
        {
            try
            {
                var productos = await _productoService.GetProductosAsync(orderBy);
                return Ok(productos);
            }
            catch (InvalidOperationProductException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo inesperado Paso.");
            }
        }

        /// <summary>
        /// Obtiene un producto especifico segun el id que es requisito enviarlo por meedio de la peticion 
        /// </summary>
        /// <param name="productoId"></param>
        /// <returns></returns>

        [HttpGet("{productoId:long}")]
        public async Task<ActionResult<ProductoModel>> GetProductoAsync(long productoId)
        {
            try
            {

                var producto = await _productoService.GetProductoAsync(productoId);
                return Ok(producto);
            }
            catch (NotFoundProductException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite crear un nuevo producto 
        /// </summary>
        /// <param name="nuevoProducto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<ProductoModel>> CreateProductAsync([FromBody] ProductoModel nuevoProducto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var producto = await _productoService.CreateProductAsync(nuevoProducto);
                return Created($"api/producto/{nuevoProducto.Id}", nuevoProducto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite actualizar un producto ya existente mediante su id
        /// </summary>
        /// <param name="productoId"></param>
        /// <param name="actualizarProducto"></param>
        /// <returns></returns>

        [HttpPut("{productoId:long}")]
        public async Task<ActionResult<ProductoModel>> UpdateProductoAsync (long productoId,[FromBody] ProductoModel actualizarProducto)
        {
            try
            {
                var producto = await _productoService.UpdateProductAsync(productoId, actualizarProducto);
                return Ok(producto);
            }
            catch (NotFoundProductException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }
        /// <summary>
        /// Permite eliminar un producto ya existente mediante su id
        /// </summary>
        /// <param name="productoId"></param>
        /// <returns></returns>

        [HttpDelete("{productoId:long}")]
        public async Task<ActionResult<bool>> DeleteProductoAsync(long productoId)
        {
            try
            {
                var respuesta = await _productoService.DeleteProductAsync(productoId);
                return Ok(respuesta);
            }
            catch (NotFoundProductException ex)
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
