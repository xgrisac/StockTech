using System.ComponentModel.DataAnnotations;

namespace WebAPI.net9.Models 
{
    /// <summary>
    /// Representa um produto com suas propriedades para criação e alteração.
    /// </summary>
    public class ProdutoModel
    {   
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome deve ter entre 1 e 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "A descrição deve ter no máximo 255 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser um número positivo.")]
        public int QuantidadeEstoque { get; set; }

        [Required(ErrorMessage = "O código de barras é obrigatório.")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "O código de barras deve ter entre 8 e 15 caracteres.")]
        public string CodigoDeBarras { get; set; } = string.Empty;

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "A marca deve ter entre 1 e 50 caracteres.")]
        public string Marca { get; set; } = string.Empty;

    }
} // string.Empty impede que o valor seja NULL, o que poderia quebrar a aplicação
