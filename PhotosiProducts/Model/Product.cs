using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PhotosiProducts.Model;

[ExcludeFromCodeCoverage]
[Table("product")]
public class Product
{
    [Column("id"), Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("category_id"), Required]
    public int CategoryId { get; set; }

    [Column("name"), Required]
    public string Name { get; set; }

    [Column("description"), Required]
    public string Description { get; set; }
    
    public virtual Category Category { get; set; }
}