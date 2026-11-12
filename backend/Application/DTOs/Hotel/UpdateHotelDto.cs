using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Hotel;

public class UpdateHotelDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Country { get; set; } = string.Empty;

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string Street { get; set; } = string.Empty;

    public int Stars { get; set; }
}