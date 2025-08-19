using System.ComponentModel.DataAnnotations;

namespace MyRecipeBook.Domain.Entities;

public class EntityBase
{
    [Key]
    public long idusers { get; set; }
    public bool Active { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}