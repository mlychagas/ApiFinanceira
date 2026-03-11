using System.ComponentModel.DataAnnotations;

namespace ApiFinanceira.Dtos
{
    public class ReceitaUpdateDto : ReceitaDto
    {
        [Required(ErrorMessage = "Situação é obrigatória")]
        [RegularExpression("^(Pendente|Recebido)$", ErrorMessage = "Situação deve ser 'Pendente' ou 'Recebido'")]
        public required string Situacao { get; set; }
        
        public DateTime? DataRecebimento { get; set; }
    }
}