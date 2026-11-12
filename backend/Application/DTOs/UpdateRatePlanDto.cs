using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class UpdateRatePlanDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }
}