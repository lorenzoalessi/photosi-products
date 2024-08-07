using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotosiProducts.Model;

[Table("category")]
public class Category
{
    [Column("id"), Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("name"), Required]
    public string Name { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}