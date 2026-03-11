using Microsoft.AspNetCore.Mvc;
using ApiFinanceira.Model;
using ApiFinanceira.Dtos;
using System.ComponentModel.DataAnnotations;

namespace ApiFinanceira.Controllers
{
    [Route("/receita")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        public static List<Receita> listaReceitas = new()
        {
            new Receita
            {
                Descricao = "Salário",
                Categoria = "Trabalho",
                Valor = 5000,
                DataPrevisao = new DateOnly(2026, 03, 05),
                Situacao = "Recebido",
                DataRecebimento = new DateTime(2026, 03, 05),
                Observacao = "Pagamento mensal"
            },
            new Receita
            {
                Descricao = "Freelance - Projeto Web",
                Categoria = "Trabalho",
                Valor = 1500,
                DataPrevisao = new DateOnly(2026, 03, 15),
                Situacao = "Pendente"
            }
        };

        [HttpGet()]
        public ActionResult FindAll()
        {
            return Ok(listaReceitas);
        }

        [HttpPost()]
        public ActionResult Create([FromBody] ReceitaDto novaReceita)
        {
            var receita = new Receita
            {
                Descricao = novaReceita.Descricao,
                Valor = novaReceita.Valor,
                Categoria = novaReceita.Categoria,
                DataPrevisao = novaReceita.DataPrevisao,
                Observacao = novaReceita.Observacao,
                Situacao = "Pendente"
            };
            
            listaReceitas.Add(receita);
            return Created($"/receita/{receita.Id}", receita);
        }

        [HttpGet("{id}")]
        public ActionResult FindById(Guid id)
        {
            var receita = listaReceitas.FirstOrDefault(r => r.Id == id);
            if (receita is null)
            {
                return NotFound(new { mensagem = $"Receita #{id} não encontrada." });
            }
            return Ok(receita);
        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, [FromBody] ReceitaUpdateDto receitaDto)
        {
            var receita = listaReceitas.FirstOrDefault(r => r.Id == id);
            if (receita is null)
            {
                return NotFound(new { mensagem = $"Receita #{id} não encontrada." });
            }

            receita.Descricao = receitaDto.Descricao;
            receita.Categoria = receitaDto.Categoria;
            receita.Valor = receitaDto.Valor;
            receita.DataPrevisao = receitaDto.DataPrevisao;
            receita.Observacao = receitaDto.Observacao;
            receita.Situacao = receitaDto.Situacao;
            receita.DataRecebimento = receitaDto.DataRecebimento;
            
            return Ok(receita);
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var receita = listaReceitas.FirstOrDefault(r => r.Id == id);
            if (receita is null)
            {
                return NotFound(new { mensagem = $"Receita #{id} não encontrada." });
            }

            listaReceitas.Remove(receita);
            return NoContent();
        }
    }
}