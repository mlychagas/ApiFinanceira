using System.ComponentModel.DataAnnotations;

namespace ApiFinanceira.Dtos
{
    public class DespesasUpdateDto : DespesaDto
    {
        [Required]
        public required string Situacao { get; set; }
        
        [Required]
        public DateTime? DataPagamento { get; set; }
    }
}
