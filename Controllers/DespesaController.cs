using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiFinanceira.Model;
using ApiFinanceira.Dtos;

namespace ApiFinanceira.Controllers
{
    [Route("/despesa")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        public static List<Despesa> listaDespesas = new()
        {
          new Despesa
          {
              Descricao = "Internet",
              Categoria = "Moradia",
              Valor = 150,
              DataVencimento =  new DateOnly(2026, 03 ,14),
              Situacao = "Aberto"

          },
          new Despesa
          {
              Descricao = "Água",
              Categoria = "Moradia",
              Valor = 50,
              DataVencimento =  new DateOnly(2026, 03 ,09),
              Situacao = "Aberto"

          }
        };

        [HttpGet()]
        public ActionResult FindAll()
        {
            return Ok(listaDespesas);
        }

        [HttpPost()]
        public ActionResult Create([FromBody] DespesaDto novaDespesa)
        {
            var despesa = new Despesa
            {
                Descricao = novaDespesa.Descricao,
                Valor = novaDespesa.Valor,
                Categoria = novaDespesa.Categoria,
                DataVencimento = novaDespesa.DataVencimento,
                Situacao = "Aberto"
            };
            
            listaDespesas.Add(despesa);

            return Created("", despesa);
        }

        [HttpGet("{id}")]
        public ActionResult FindById(Guid id)
        {
            var despesa = listaDespesas.FirstOrDefault(d => d.Id == id);
            if (despesa is null)
            {
                return NotFound(new { mensagem = $"Despesa #{id} não encontrada." });
            }
            return Ok(despesa);
        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, [FromBody] DespesasUpdateDto despesaDto)
        {
            var despesa = listaDespesas.FirstOrDefault(d => d.Id == id);
            if (despesa is null)
            {
                return NotFound(new { mensagem = $"Despesa #{id} não encontrada." });
            }

            despesa.Descricao = despesaDto.Descricao;
            despesa.Categoria = despesaDto.Categoria;
            despesa.Valor = despesaDto.Valor;
            despesa.DataVencimento = despesaDto.DataVencimento;
            despesa.Situacao = despesaDto.Situacao;
            despesa.DataPagamento = despesaDto.DataPagamento;
            return Ok(despesa);
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var despesa = listaDespesas.FirstOrDefault(d => d.Id == id);
            if (despesa is null)
            {
                return NotFound(new { mensagem = $"Despesa #{id} não encontrada." });
            }

            listaDespesas.Remove(despesa);
            return NoContent();
            
        }
    }
}
