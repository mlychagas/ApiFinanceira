using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiFinanceira.Model;
using ApiFinanceira.Dtos;
using ApiFinanceira.DataContexts;
using Microsoft.EntityFrameworkCore;
using ApiFinanceira.Services;
using ApiFinanceira.Exceptions;

namespace ApiFinanceira.Controllers
{
    [Route("/despesa")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly AppDbContex _context;
        private readonly DespesaService _servise;
        public DespesaController( DespesaService service ,AppDbContex context)
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




        //public static List<Despesa> listaDespesas = new()
        //{
        //  new Despesa
        //  {
        //      Descricao = "Internet",
        //      Categoria = "Moradia",
        //      Valor = 150,
        //      DataVencimento =  new DateOnly(2026, 03 ,14),
        //      Situacao = "Aberto"

        //  },
        //  new Despesa
        //  {
        //      Descricao = "Água",
        //      Categoria = "Moradia",
        //      Valor = 50,
        //      DataVencimento =  new DateOnly(2026, 03 ,09),
        //      Situacao = "Aberto"

        //  }
        //};



        //[HttpPost()]
        //public ActionResult Create(string id, [FromBody] DespesaDto novaDespesa)
        //{
        //    var despesa = new Despesa
        //    {
        //        Descricao = novaDespesa.Descricao,
        //        Valor = novaDespesa.Valor,
        //        Categoria = novaDespesa.Categoria,
        //        DataVencimento = novaDespesa.DataVencimento,
        //        Situacao = "Aberto"
        //    };

        //    listaDespesas.Add(despesa);

        //    return Created("", despesa);
        //}

        //[HttpGet("{id}")]
        //public ActionResult FindById(Guid id)
        //{
        //    var despesa = listaDespesas.FirstOrDefault(d => d.Id == id);
        //    if (despesa is null)
        //    {
        //        return NotFound(new { mensagem = $"Despesa #{id} não encontrada." });
        //    }
        //    return Ok(despesa);
        //}

        //[HttpPut("{id}")]
        //public ActionResult Update(Guid id, [FromBody] DespesasUpdateDto despesaDto)
        //{
        //    var despesa = listaDespesas.FirstOrDefault(d => d.Id == id);
        //    if (despesa is null)
        //    {
        //        return NotFound(new { mensagem = $"Despesa #{id} não encontrada." });
        //    }

        //    despesa.Descricao = despesaDto.Descricao;
        //    despesa.Categoria = despesaDto.Categoria;
        //    despesa.Valor = despesaDto.Valor;
        //    despesa.DataVencimento = despesaDto.DataVencimento;
        //    despesa.Situacao = despesaDto.Situacao;
        //    despesa.DataPagamento = despesaDto.DataPagamento;
        //    return Ok(despesa);
        //}

        //[HttpDelete("{id}")]
        //public ActionResult Remove(Guid id)
        //{
        //    var despesa = listaDespesas.FirstOrDefault(d => d.Id == id);
        //    if (despesa is null)
        //    {
        //        return NotFound(new { mensagem = $"Despesa #{id} não encontrada." });
        //    }

        //    listaDespesas.Remove(despesa);
        //    return NoContent();

        //}
    }
}
