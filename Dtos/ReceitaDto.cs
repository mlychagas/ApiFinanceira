using System.ComponentModel.DataAnnotations;

namespace ApiFinanceira.Dtos
{
    public class ReceitaDto
    {
        [Required(ErrorMessage = "Descrição é obrigatória")]
        [MinLength(5, ErrorMessage = "Obrigatório mínimo de 5 caracteres")]
        public required string Descricao { get; set; }
        
        [Required(ErrorMessage = "Valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public required decimal Valor { get; set; }
        
        [Required(ErrorMessage = "Data de Previsão é obrigatória")]
        public DateOnly DataPrevisao { get; set; }
        
        [Required(ErrorMessage = "Categoria é obrigatória")]
        public required string Categoria { get; set; }
        
        [MaxLength(500, ErrorMessage = "Observação não pode exceder 500 caracteres")]
        public string? Observacao { get; set; }
    }
}