using System.ComponentModel.DataAnnotations;

namespace MyRecipeBook.Domain.Entities;

public class EntityBase
{
    [Key]  /// ao adicionar esse key indica pro entityframework que essa e chave primaria para registro na tabela users 
    public long idusers { get; set; }
    public bool Active { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}