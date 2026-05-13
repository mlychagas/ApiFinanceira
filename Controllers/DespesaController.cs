using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiFinanceira.Model;
using ApiFinanceira.Dtos;
using ApiFinanceira.DataContexts;
using Microsoft.EntityFrameworkCore;
using ApiFinanceira.Services;
using ApiFinanceira.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace ApiFinanceira.Controllers
{
    [Route("/despesa")]
    [ApiController]
    [Authorize]
    public class DespesaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly DespesaService _servise;
        public DespesaController( DespesaService service ,AppDbContext context)
        {
            _context = context;
            _servise = service;
        }

        [HttpGet()]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                var despesas = await _servise.FindAll();
                return Ok(despesas);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            try
            {
                var despesa = await _servise.FindById(id);
                return Ok(despesa);
            }
            catch(ErrorServiceException e)
            {
                return e.ToActionResult(this);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] DespesaDto novaDespesa)
        {
            try
            {
                var despesa = await _servise.Create(novaDespesa);
                return Created("", despesa);

            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DespesasUpdateDto despesaDto)
        {
            try
            {
                var despesa =  await _servise.Update(id, despesaDto);
                return Ok(despesa);
            }
            catch (ErrorServiceException e)
            {
                return e.ToActionResult(this);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }

        [HttpPost("{id}/tags")]
        public async Task<IActionResult> AddTags(int id, [FromBody] DespesaTagDto tag)
        {
            try
            {
                var despesa = await _servise.AddTags(id, tag);
                return Ok();
            }
            catch (Exception ex )
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            try
            {
                await _servise.Remove(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }


    }
}
