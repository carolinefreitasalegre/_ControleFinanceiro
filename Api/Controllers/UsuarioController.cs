using Application.Contracts;
using Application.Dtos.Request;
using Application.Dtos.Response;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<UsuarioInput> _validator;
        private readonly IValidator<UsuarioEdicaoInput> _validatorEdicao;

        public UsuarioController(IUsuarioService usuarioService, IValidator<UsuarioInput> validator, IValidator<UsuarioEdicaoInput> validatorEdicao)
        {
            _usuarioService = usuarioService;
            _validator = validator;
            _validatorEdicao = validatorEdicao;
        }



        [HttpGet("usuarios")]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var usuarios = await _usuarioService.BuscarUsuarios();

            return Ok(usuarios);
        }

        [HttpGet("buscar-usuario/{Id}")]
        public async Task<IActionResult> BuscarUsuarioId(Guid Id)
        {
            var usuario = await _usuarioService.BuscarUsuarioId(Id);

            if (usuario == null)
                return BadRequest("Usuário não encontrado");

            return Ok(usuario);
        }

        [HttpPost("criar-usuario")]
        public async Task<IActionResult> CriarUsuario(UsuarioInput input)
        {

            var validator = await _validator.ValidateAsync(input);

            if (!validator.IsValid)
                return BadRequest(validator.Errors);

            var usuario = await _usuarioService.CriarUsuario(input);

            return Created("", usuario);
        }


        [HttpPost("editar-usuario/{Id}")]
        public async Task<IActionResult> EditarUsuario(Guid Id, UsuarioEdicaoInput input)
        {

            var usuario = await _usuarioService.BuscarUsuarioId(Id);

            if (usuario == null)
                return BadRequest("Usuário não encontrado");

            var validator = await _validatorEdicao.ValidateAsync(input);

            if (!validator.IsValid)
                return BadRequest(validator.Errors);

            await _usuarioService.EditarUsuario(Id, input);

            return Created("", input);

        }


    }
}
