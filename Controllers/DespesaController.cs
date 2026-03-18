using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiFinanceira.Model;
using ApiFinanceira.Dtos;
using ApiFinanceira.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanceira.Controllers
{
    [Route("/despesa")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly AppDbContex _context;
        public DespesaController(AppDbContex context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                var listaDespesas = await _context.Despesas.ToListAsync();
                return Ok(listaDespesas);
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
                var despesa = new Despesa
                {
                    Descricao = novaDespesa.Descricao,
                    Valor = novaDespesa.Valor,
                    Categoria = novaDespesa.Categoria,
                    DataVencimento = novaDespesa.DataVencimento,
                    Situacao = "pendente"
                };

                await _context.Despesas.AddAsync(despesa);
                await _context.SaveChangesAsync();
                return Created($"/despesa/{despesa.Id}", despesa);

            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindAll(int id)
        {
            try
            {
                var despesa = await _context.Despesas.FirstOrDefaultAsync(x => x.Id == id);
                if(despesa is null)
                {
                    return NotFound(new { mensagem = $"Despesa #{id} não encontrada."});
                }
                return Ok(despesa);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] DespesasUpdateDto despesaDto)
        {
            try
            {
                var despesa =  await _context.Despesas.FirstOrDefaultAsync(x => x.Id == id);
                if (despesa is null)
                {
                    return NotFound(new { mensagem = $"Despesa #{id} não encontrada." });
                }

                
                despesa.Descricao = despesaDto.Descricao;
                despesa.Valor = despesaDto.Valor;
                despesa.DataVencimento = despesaDto.DataVencimento;
                despesa.Categoria = despesaDto.Categoria;
                despesa.Situacao = despesaDto.Situacao;
                despesa.DataPagamento = despesaDto.DataPagamento;

                _context.Despesas.Update(despesa);
                await _context.SaveChangesAsync();
                return Ok(despesa);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }

        [HttpDelete]
        public async Task<ActionResult> Remove(int id)
        {
            try
            {
                var despesa = await _context.Despesas.FirstOrDefaultAsync(x => x.Id == id);
                if (despesa is null)
                {
                    return NotFound(new { mensagem = $"Despesa #{id} não encontrada." });
                }

                _context.Despesas.Remove(despesa);
                await _context.SaveChangesAsync();
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
