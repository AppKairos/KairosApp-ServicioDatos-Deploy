using AutoMapper;
using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.BusinessLogicLayer.Models.Security;
using ImprentaAPI.BusinessLogicLayer.Services;
using ImprentaAPI.DataAccessLayer.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, ITokenService tokenService, IMapper mapper)
        {
            _repo = repo;
            _tokenService = tokenService;
            _mapper=mapper;
        }
        /// <summary>
        /// Permite registrar un nuevo cliente con el rol definido por defecto de este, debido a que los roles de administrador solo son creados por comandos sql
        /// Se debe ingresar un modelo de usuario que tena ci,nombre,correo,telefono y password los demas atributos son llenados por defecto en el backend
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult>Register(UsuarioModel usuario)
        {
            usuario.Email = usuario.Email.ToLower();
            if(await _repo.ExisteUsuario(usuario.Email))
            {
                return BadRequest("Usuario con ese Ci ya esta registrado");
            }
            var usuarioCreado = await _repo.RegistrarAsync(usuario,usuario.Password);
            if(usuarioCreado==null)
            {
                return BadRequest("Usuario con el correo ya esta registrado");
            }
            return Ok(usuarioCreado);

        }
        /// <summary>
        /// Permite hacer login a un usuario o administrador para usar las caracteristicas de la API este end point solo necesita un ci (Carnet de identidad y contraseña y devuelve un token con los 
        /// atributos del usuario si es que este esta registrado
        /// </summary>
        /// <param name="usuarioLoginModel"></param>
        /// <returns></returns>

        [HttpPost("Login")]
        public async Task<IActionResult>Login(UsuarioLoginModel usuarioLoginModel)
        {
            var usuarioFromRepo = await _repo.LoginAsync(usuarioLoginModel.Email, usuarioLoginModel.Password);
            if (usuarioFromRepo==null)
            {
                return Unauthorized();
            }
            var userToShow = await _repo.LoginToShowAsync(usuarioLoginModel.Email);
            var token = _tokenService.CreateToken(usuarioFromRepo);
            return Ok(new
            {
                token = token,
                usuario = userToShow
            });
        }


        /// <summary>
        /// Permite registrar un nuevo cliente con el rol definido por defecto de este, debido a que los roles de administrador solo son creados por comandos sql
        /// Se debe ingresar un modelo de usuario que tena ci,nombre,correo,telefono y password los demas atributos son llenados por defecto en el backend
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost("register/gmail")]
        public async Task<IActionResult> RegisterGmail(UsuarioGmailModel usuario)
        {
            if (await _repo.ExisteUsuarioGmail(usuario.Email))
            {
                return BadRequest("Usuario con este email ya esta registrado");
            }
            var usuarioCreado = await _repo.RegistrarGmailAsync(usuario);
            if (usuarioCreado == null)
            {
                return BadRequest("Usuario con el correo ya esta registrado");
            }
            return Ok(usuarioCreado);

        }
        /// <summary>
        /// Permite hacer login a un usuario o administrador para usar las caracteristicas de la API este end point solo necesita un ci (Carnet de identidad y contraseña y devuelve un token con los 
        /// atributos del usuario si es que este esta registrado
        /// </summary>
        /// <param name="usuarioGmailLoginModel"></param>
        /// <returns></returns>

        [HttpPost("Login/gmail")]
        public async Task<IActionResult> LoginGmail(UsuarioGmailModel usuarioLoginModel)
        {
            try
            {
                var usuarioFromRepo = await _repo.LoginGmailAsync(usuarioLoginModel);
                if (usuarioFromRepo == null)
                {
                    return Unauthorized();
                }
                if (usuarioFromRepo.TipoUsuario.ToLower() == "manual")
                {
                    UsuarioModel usuarioFromRepoI = new UsuarioModel();
                    usuarioFromRepoI.Id = usuarioFromRepo.Id;
                    usuarioFromRepoI.Nombre = usuarioFromRepo.Nombre;
                    usuarioFromRepoI.Email = usuarioFromRepo.Email;
                    usuarioFromRepoI.Rol = usuarioFromRepo.Rol;
                    usuarioFromRepoI.Activo = usuarioFromRepo.Activo;
                    usuarioFromRepo.PasswordHash = null;
                    usuarioFromRepo.PasswordSalt = null;
                    usuarioFromRepoI.Telefono = usuarioFromRepo.Telefono;
                    usuarioFromRepoI.Password = "";
                    usuarioFromRepoI.TipoUsuario = usuarioFromRepo.TipoUsuario;
                    var token = _tokenService.CreateToken(usuarioFromRepoI);
                    return Ok(new
                    {
                        token = token,
                        usuario = usuarioFromRepo
                    });
                }
                else
                {
                    if (usuarioFromRepo.TipoUsuario.ToLower() == "gmail")
                    {
                        var token = usuarioFromRepo.IdToken;
                        usuarioFromRepo.IdToken = null;
                        return Ok(new
                        {
                            token = token,
                            usuario = usuarioFromRepo
                        });
                    }
                    return Unauthorized();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }

        }


        /// <summary>
        /// Permite la eliminacion de un perfil de la tabla de usuarios pasando el id
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns></returns>

        [HttpDelete("{clienteId:long}")]
        public async Task<ActionResult<bool>> DeleteClienteAsync(long clienteId)
        {
            try
            {
                var respuesta = await _repo.DeleteClienteAsync(clienteId);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }

        }


        /// <summary>
        /// Permite la eliminacion de un perfil de la tabla de usuarios gmail pasando el id
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns></returns>

        [HttpDelete("deleteAccountGmail/{clienteId:long}")]
        public async Task<ActionResult<bool>> DeleteClienteGmailAsync(long clienteId)
        {
            try
            {
                var respuesta = await _repo.DeleteClienteGmailAsync(clienteId);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>

        [HttpGet("{usuarioId:long}")]
        public async Task<ActionResult<IEnumerable<UsuarioToShowModel>>> GetUsuariosAsync(long usuarioId)
        {
            try
            {
                var usuarios = await _repo.GetUsuariosAsync(usuarioId);
                if(usuarios!=null)
                {
                    return Ok(usuarios);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent, "No autorizado.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo inesperado Paso.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet("getUsuariosGmail/{usuarioId:long}")]
        public async Task<ActionResult<IEnumerable<UsuarioToShowModel>>> GetUsuariosGmailAsync(long usuarioId)
        {
            try
            {
                var usuarios = await _repo.GetUsuariosGmailAsync(usuarioId);
                if (usuarios != null)
                {
                    return Ok(usuarios);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent, "No autorizado.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo inesperado Paso.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpGet("user/{usuario:long}")]
        public async Task<ActionResult<EmpastadoModel>> GetUsuarioAsync(long usuario)
        {
            try
            {
                var usuario_to_show = await _repo.GetUsuarioAsync( usuario);
                return Ok(usuario_to_show);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpGet("userGmail/{usuario:long}")]
        public async Task<ActionResult<EmpastadoModel>> GetUsuarioGmailAsync(long usuario)
        {
            try
            {
                var usuario_to_show = await _repo.GetUsuarioGmailAsync( usuario);
                return Ok(usuario_to_show);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }

        }


        [HttpPut("user/{usuarioId:long}")]
        public async Task<ActionResult<UsuarioToShowModel>> UpdateUsuarioAsync(long usuarioId, [FromBody] UsuarioToShowModel usuarioActualizar)
        {
            try
            {
                var usuario = await _repo.UpdateUsuarioAsync(usuarioId, usuarioActualizar);
                return Ok(usuario);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }

        [HttpPut("userGmail/{usuarioId:long}")]
        public async Task<ActionResult<UsuarioToShowModel>> UpdateUsuarioGmailAsync(long usuarioId, [FromBody] UsuarioToShowGmailModel usuarioActualizar)
        {
            try
            {
                var usuario = await _repo.UpdateUsuarioGmailAsync(usuarioId, usuarioActualizar);
                return Ok(usuario);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso.");
            }
        }

































    }
}
