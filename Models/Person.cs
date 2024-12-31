
using System.ComponentModel.DataAnnotations;

namespace UxcomexTest.Models;

public class Person
{
    public int Id { get; set; }

    [StringLength(60, MinimumLength = 3)]
    [Required]
    [Display(Name = "Nome")]
    public required string Name { get; set; }
    // [StringLength(19, MinimumLength = 19)]
    // [StringLength(13, MinimumLength = 13)]
    [Required]
    [Display(Name = "Telefone")]
    public required string PhoneNumber { get; set; }
    // [StringLength(14, MinimumLength = 14)]
    // [StringLength(9, MinimumLength = 9)]
    [Required]
    public required string CPF { get; set; }
    public List<Address>? Addresses { get; set; }

    internal string? ToList()
    {
        throw new NotImplementedException();
    }
}

