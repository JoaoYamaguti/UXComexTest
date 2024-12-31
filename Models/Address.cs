
using System.ComponentModel.DataAnnotations;

namespace UxcomexTest.Models;

public class Address
{
    public int Id { get; set; }
    [StringLength(50)]
    [Required]
    [Display(Name = "Endereço")]
    public required string Name { get; set; }
    [StringLength(50)]
    [Required]
    [Display(Name = "Cidade")]
    public required string City { get; set; }
    [StringLength(50)]
    [Required]
    [Display(Name = "Estado")]
    public required string State { get; set; }
    // [StringLength(9, MinimumLength = 9)]
    // [StringLength(8, MinimumLength = 8)]
    [Required]
    public required string CEP { get; set; }

    public required int PersonId { get; set; }

}
