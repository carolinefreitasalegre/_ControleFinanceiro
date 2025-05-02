using Application.Contracts;
using Application.Dtos.Request;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
   
    public class ContaController : ControllerBase
    {
       
        private readonly IContaService _contaService;
        private readonly IValidator<ContaInput> _validator;
        private readonly IValidator<ContaEdicaoInput> _validatorEdicao;

        public ContaController(IContaService contaService, IValidator<ContaInput> validator, IValidator<ContaEdicaoInput> validatorEdicao)
        {
            _contaService = contaService;
            _validator = validator;
            _validatorEdicao = validatorEdicao;
        }


        [HttpGet("contas")]
        public async Task<IActionResult> BuscarContas()
        {
            var contas =  await _contaService.BuscarContas();

            return Ok(contas);
        }

        [HttpGet("buscar-conta/{Id}")]
        public async Task<IActionResult> BuscarContaId(Guid Id)
        {
            var conta = await _contaService.BuscarContaId(Id);

            if(conta == null) 
                return NotFound("Conta não encontrada.");

            return Ok(conta);
        }

        [HttpPost("criar-conta")]
        public async Task<IActionResult> CriarConta([FromBody] ContaInput input)
        {
            var validator = _validator.Validate(input);

            if (!validator.IsValid)
                return BadRequest(validator.Errors);

            await _contaService.CriarConta(input);

            return Ok(input);

        }

        [HttpPost("editar-conta/{Id}")]
        public async Task<IActionResult> EditarConta(Guid Id, ContaEdicaoInput input)
        {

            var validator = await _validatorEdicao.ValidateAsync(input);

            if (!validator.IsValid)
                return BadRequest(validator.Errors);

            var conta = await _contaService.BuscarContaId(Id);

            if (conta == null)
                return NotFound("Conta não encontrada.");

            await _contaService.EditarConta(Id, input);

            return Ok(conta);   
        }
    }
}
