using System.ComponentModel.DataAnnotations;

namespace DesafioLivrariaAPICSharp.Models;

/// <summary>
/// Representa um livro na livraria.
/// </summary>
public class Book
{
    /// <summary>
    /// Identificador único. Será chave primária.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Título do livro.
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Nome do autor do livro.
    /// </summary>
    [Required]
    [StringLength(150)]
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// Gênero ou categoria do livro.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Genre { get; set; } = string.Empty;

    /// <summary>
    /// Preço de venda do livro (valor mínimo 0).
    /// </summary>
    [Range(0.0, double.MaxValue)]
    public decimal Price { get; set; }

    /// <summary>
    /// Quantidade em estoque (valor mínimo 0).
    /// </summary>
    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }
}