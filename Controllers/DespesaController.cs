using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiTeste.Model;
using ApiTeste.Dtos;
namespace ApiTeste.Controllers
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
            return Ok(despesa);
        }


    }
}
