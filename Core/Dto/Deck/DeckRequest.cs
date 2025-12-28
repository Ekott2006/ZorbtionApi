using System.ComponentModel.DataAnnotations;

namespace Core.Services;

public class DeckRequest
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 5)]
    public string Description { get; set; }
    
    public DeckOptionRequest? OptionRequest { get; set; }
}