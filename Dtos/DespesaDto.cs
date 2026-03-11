using System.ComponentModel.DataAnnotations;

namespace ApiFinanceira.Dtos
{
    public class DespesaDto
    {
        [Required(ErrorMessage = "Descrição é obrigatório")]
        [MinLength(5, ErrorMessage = "Obrigatório mínimo de 5 caracteres")]
        public required string Descricao { get; set; }
        
        [Required(ErrorMessage = "Categoria é obrigatório")]
        public required string Categoria { get; set; }
        
        [Required(ErrorMessage = "Valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public required decimal Valor { get; set; }
        
        [Required(ErrorMessage = "DataVencimento é obrigatório")]
        public DateOnly DataVencimento { get; set; }
    }
}
